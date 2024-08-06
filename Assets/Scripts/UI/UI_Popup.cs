using UnityEngine;

public class UI_Popup : UI_Base
{
    protected override void OnEnable()
    {
        base.OnEnable();

        System.GC.Collect();
        FlowManager.Instance.Player.GetComponent<PlayerMovementAdvanced>().DeactiveInput();
    }

    protected override void OnDisable()
    {
        FlowManager.Instance.Player.GetComponent<PlayerMovementAdvanced>().ActiveInput();
    }
}
