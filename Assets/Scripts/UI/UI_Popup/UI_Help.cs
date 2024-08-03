using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Help : UI_Popup
{
    enum Buttons
    {
        UI_Help_Btn_1,
        UI_Help_Btn_2,
        UI_Help_Btn_3
    }

    enum Texts
    {
        UI_Help_Text_Title,
        UI_Help_Text_SubTitle,
        UI_Help_Text_ESC,
        UI_Help_Btn_1,
        UI_Help_Btn_2,
        UI_Help_Btn_3,
        UI_Help_Btn_4,
    }

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Buttons));
        AddAllTextToLocalizeStringEvent();
    }


}
