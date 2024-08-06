using UnityEngine;

public class SceneChangeObject : InteractionableObject
{
    public GameEnum.SceneName _sceneName;

    protected override void Interaction(Transform fromTransform)
    {
        base.Interaction(fromTransform);

        if(_sceneName != GameEnum.SceneName.MainScene)
        {
            UI_Main main = UIManager.Instance.StaticUI as UI_Main;
            if(main != null)
            {
                main._mainScenePlayerPosition = FlowManager.Instance.Player.transform.position;
            }
        }
        LoadingScene.LoadScene(_sceneName.ToString());
    }
}
