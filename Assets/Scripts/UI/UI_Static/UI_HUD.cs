using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using System.Collections.Generic;

public class UI_HUD : UI_Static
{
    [SerializeField] private GameObject _interactionBoxObject;
    [SerializeField] private GameObject _alarmTextPrefap;
    [SerializeField] private GameObject _alarmBoxObject;
    [SerializeField] private TMP_Text _interactionText;

    public int _maxAlarmMessageCount = 5;
    private Queue<GameObject> _alarmTextQueue = new Queue<GameObject>();

    private string _clearText = "";
    private int _currentID = -1;

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
