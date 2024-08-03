using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class InteractionableObject : Interactionable
{
    [SerializeField] private int _requireItemid = 0;
    private bool _alreadyInteraction = false;
    private static string _failedAlarmString1 = "Alarm_RequiredItem_1";
    private static string _failedAlarmString2 = "Alarm_RequiredItem_2";

    protected override void Interaction(Transform fromTransform)
    {
        base.Interaction(fromTransform);

        if (_alreadyInteraction == true)
        {
            OverInteractionCount();
            return;
        }

        if(_requireItemid == 0)
        {
            _alreadyInteraction = true;
            ProgressInteraction();
        }
        else
        {
            if (FlowManager.Instance.IsHaveCollectableItem(_requireItemid))
            {
                _alreadyInteraction = true;
                ProgressInteraction();
            }
            else
            {
                Locale currentLanguage = LocalizationSettings.SelectedLocale;
                string message1 = LocalizationSettings.StringDatabase.GetLocalizedString("Misc", _failedAlarmString1, currentLanguage);
                string message2 = LocalizationSettings.StringDatabase.GetLocalizedString("Misc", _failedAlarmString2, currentLanguage);
                string itemName = LocalizationSettings.StringDatabase.GetLocalizedString("InteractionableObject", _requireItemid.ToString(), currentLanguage);
                EventHandler.CallAddAlarmMessageEvent(message1 + itemName + message2);
            }
        }

        
    }

    protected virtual void OverInteractionCount()
    {

    }

    protected virtual void ProgressInteraction()
    {

    }
}
