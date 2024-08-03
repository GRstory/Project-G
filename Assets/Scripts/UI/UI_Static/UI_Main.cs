using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UI_Main : UI_Static
{
    [SerializeField] private UI_Static _hudUI;

    enum Buttons
    {
        UI_Main_Btn_1,
        UI_Main_Btn_2,
        UI_Main_Btn_3
    }

    enum Texts
    {
        UI_Main_Btn_1,
        UI_Main_Btn_2,
        UI_Main_Btn_3
    }


    protected override void Start()
    {
        base.Start();

        UIManager.Instance.ChangeStaticUI(this);

        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Buttons));
        
        AddAllTextToLocalizeStringEvent();
        BindAllButtonToOnClickFunc();
    }

    public void UI_Main_Btn_1()
    {
        UIManager.Instance.ChangeStaticUI(_hudUI);
        LoadingScene.LoadScene("TestScene");
    }

    public void UI_Main_Btn_2()
    {
        UIManager.Instance.OnActivePopupUI(UIManager.Instance.AchievementUI);
    }

    public void UI_Main_Btn_3()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
