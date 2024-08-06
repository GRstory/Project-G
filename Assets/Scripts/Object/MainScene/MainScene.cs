using UnityEngine;

public class MainScene : SceneController
{ 
    protected override void OnEnable()
    {
        base.OnEnable();
        UIManager.Instance.ChangeStaticUI(UIManager.Instance.MainUI);
    }
}
