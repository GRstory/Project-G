using System;
using UnityEngine;

public static class EventHandler
{
    public static event Action<string> AddAlarmMessageEvent;
    public static event Action ExcludeAlarmMessageEvent;
    public static event Action<int> ChangeInteractionableText;
    public static event Action<int> ClearAchievement;

    public static void CallAddAlarmMessageEvent(string message)
    {
        if(AddAlarmMessageEvent != null)
        {
            AddAlarmMessageEvent(message);
        }
    }

    public static void CallExcludeAlarmMessageEvent()
    {
        if(ExcludeAlarmMessageEvent != null)
        {
            ExcludeAlarmMessageEvent();
        }
    }

    public static void CallChangeInteractionableText(int index)
    {
        if(ChangeInteractionableText != null)
        {
            ChangeInteractionableText(index);
        }
    }

    public static void CallClearAchievement(int index)
    {
        if (ClearAchievement != null)
        {
            ClearAchievement(index);
        }
    }
}
