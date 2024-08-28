using UnityEngine;
using UnityEngine.SceneManagement;
using static GameEnum;

public class FinishDoor : InteractionableObject
{
    protected override void OverInteractionCount()
    {
        
    }

    protected override void ProgressInteraction()
    {
        SceneController sceneController = FlowManager.Instance.SceneController;
        if(sceneController.IsClearScene())
        {
            if (SceneManager.GetActiveScene().name != GameEnum.SceneName.MainScene.ToString())
            {
                LoadingScene.LoadScene(GameEnum.SceneName.MainScene.ToString());
            }
        }
    }
}
