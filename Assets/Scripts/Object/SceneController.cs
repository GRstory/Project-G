using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public abstract class SceneController : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] protected bool _isClear = false;


    protected virtual void OnEnable()
    {
        FlowManager.Instance.Player.transform.position = _spawnPoint.position;
        FlowManager.Instance.Player.transform.rotation = _spawnPoint.rotation;
        FlowManager.Instance.SceneController = this;
        StartCoroutine(PlayerSpawnPointCoroutine());
    }

    private IEnumerator PlayerSpawnPointCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        FlowManager.Instance.Player.transform.position = _spawnPoint.position;
        FlowManager.Instance.Player.transform.rotation = _spawnPoint.rotation;
    }

    public void SetClear()
    {
        _isClear = true;

        StartCoroutine(CallAlarmCoroutine());
    }

    private IEnumerator CallAlarmCoroutine()
    {
        Locale currentLanguage = LocalizationSettings.SelectedLocale;
        string message1 = LocalizationSettings.StringDatabase.GetLocalizedString("Misc", "Alarm_Clear_1", currentLanguage);
        string message2 = LocalizationSettings.StringDatabase.GetLocalizedString("Misc", "Alarm_Clear_2", currentLanguage);

        EventHandler.CallAddAlarmMessageEvent(message1);
        yield return new WaitForSeconds(0.5f);
        EventHandler.CallAddAlarmMessageEvent(message2);
    }

    public abstract bool IsClearScene();
}
