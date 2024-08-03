using UnityEngine;

public class UI_Popup : UI_Base
{
    protected override void OnEnable()
    {
        base.OnEnable();

        System.GC.Collect();
    }
}
