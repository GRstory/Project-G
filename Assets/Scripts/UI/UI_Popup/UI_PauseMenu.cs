using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseMenu : UI_Popup
{
    [SerializeField] UI_Popup settingUI;
    [SerializeField] UI_Popup helpUI;

    protected override void OnEnable()
    {
        base.OnEnable();

        Time.timeScale = 0f;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Time.timeScale = 1f;
    }

    public void OnButtonClick_Button1()
    {
        UIManager.Instance.OnActivePopupUI(settingUI);
    }

    public void OnButtonClick_Button2()
    {
        UIManager.Instance.OnActivePopupUI(helpUI);
    }

    public void OnButtonClick_Button3()
    {
        UIManager.Instance.ChangeStaticUI(UIManager.Instance.MainUI);
        FlowManager.Instance.Player.GetComponent<PlayerMovementAdvanced>().DeactiveInput();

        if (SceneManager.GetActiveScene().name != GameEnum.SceneName.MainScene.ToString())
        {
            LoadingScene.LoadScene(GameEnum.SceneName.MainScene.ToString());
        }
    }

    public void OnButtonClick_Button4()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
