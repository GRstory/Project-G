using System;
using UnityEngine;

public class TestAddAlarm : InteractionableObject
{
    private void Awake()
    {
        _coolDownTime = 0.3f;
    }

    protected override void OverInteractionCount()
    {
        
    }

    protected override void ProgressInteraction()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        if (rand == 0)
        {
            EventHandler.CallAddAlarmMessageEvent($"Call Time: {DateTime.Now}");
        }
        else if (rand == 1)
        {
            EventHandler.CallAddAlarmMessageEvent("TESTTESTTESTTEST : Random Message");
        }
        else if (rand == 2)
        {
            EventHandler.CallAddAlarmMessageEvent("Hello World");
        }
    }
}
