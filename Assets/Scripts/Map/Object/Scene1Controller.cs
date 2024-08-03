using UnityEngine;

public class Scene1Controller : MapController
{
    public UI_Static _newUIStatic;

    protected override void Start()
    {
        UIManager.Instance.ChangeStaticUI(_newUIStatic);
    }
}
