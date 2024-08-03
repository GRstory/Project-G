using Unity.VisualScripting;
using UnityEngine;

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
        LoadingScene.LoadScene("MainScene");
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
