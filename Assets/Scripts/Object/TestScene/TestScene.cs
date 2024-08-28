using UnityEngine;

public class TestScene : SceneController
{

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override bool IsClearScene()
    {
        return _isClear;
    }
}
