using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UI_HUD : UI_Static
{
    [SerializeField] private GameObject _interactionBoxObject;
    [SerializeField] private TMP_Text _interactionText;

    [SerializeField] private GameObject _alarmBoxObject;
    private GameObject _alarmTextPrefap;

    public int _maxAlarmMessageCount = 5;
    private Queue<GameObject> _alarmTextQueue = new Queue<GameObject>();

    private string _clearText = "";
    private int _currentID = -1;

    private AsyncOperationHandle _handle;

    protected override void Start()
    {
        base.Start();

        Addressables.LoadAssetAsync<GameObject>("UI_HUD_Alarm").Completed += (AsyncOperationHandle<GameObject> handle) => 
        { 
            _handle = handle;
            _alarmTextPrefap = handle.Result;
        };
    }

    protected override void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        EventHandler.AddAlarmMessageEvent -= AddAlarmMessage;
        EventHandler.AddAlarmMessageEvent += AddAlarmMessage;
        EventHandler.ExcludeAlarmMessageEvent -= ExcludeAlarmMessage;
        EventHandler.ExcludeAlarmMessageEvent += ExcludeAlarmMessage;
        EventHandler.ChangeInteractionableText -= SetInteractionText;
        EventHandler.ChangeInteractionableText += SetInteractionText;
    }

    protected override void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        EventHandler.AddAlarmMessageEvent -= AddAlarmMessage;
        EventHandler.ExcludeAlarmMessageEvent -= ExcludeAlarmMessage;
        EventHandler.ChangeInteractionableText -= SetInteractionText;

        while(_alarmTextQueue.Count != 0)
        {
            GameObject obj = _alarmTextQueue.Dequeue();
            Destroy(obj);
        }
    }

    private void SetInteractionText(int id)
    {
        if(_currentID != id)
        {
            _currentID = id;
            string stoi = id.ToString();
            Locale currentLanguage = LocalizationSettings.SelectedLocale;
            string text = LocalizationSettings.StringDatabase.GetLocalizedString("InteractionableObject", stoi, currentLanguage);

            if(text == "-")
            {
                _interactionText.text = _clearText;
            }
            else
            {
                _interactionText.text = text;
            }
        }
    }


    private void AddAlarmMessage(string message)
    {
        GameObject newAlarmObject = Instantiate(_alarmTextPrefap, _alarmBoxObject.transform);
        newAlarmObject.GetComponent<UI_AlarmText>().SetAlarmMessage(message);
        _alarmTextQueue.Enqueue(newAlarmObject);

        if(_alarmTextQueue.Count > _maxAlarmMessageCount)
        {
            Destroy(_alarmTextQueue.Dequeue());
        }
    }

    private void ExcludeAlarmMessage()
    {
        _alarmTextQueue.Dequeue();
    }
}
