using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UI_Achievement : UI_Popup
{
    enum Texts
    {
        UI_Achievement_Text_Title,
        UI_Achievement_Text_SubTitle, 
        UI_Achievement_Text_ESC,
        UI_Achievement_Text_Blank,
        UI_Achievement_Text_Blank_Description,
    }
    enum Buttons
    {
        UI_Achievement_Btn_0,
        UI_Achievement_Btn_1,
        UI_Achievement_Btn_2,
        UI_Achievement_Btn_3,
        UI_Achievement_Btn_4,
        UI_Achievement_Btn_5,
        UI_Achievement_Btn_6,
        UI_Achievement_Btn_7,
        UI_Achievement_Btn_8,
        UI_Achievement_Btn_9,
        UI_Achievement_Btn_10,
        UI_Achievement_Btn_11,
        UI_Achievement_Btn_12,
        UI_Achievement_Btn_13,
        UI_Achievement_Btn_14,
        UI_Achievement_Btn_15,
        UI_Achievement_Btn_16,
        UI_Achievement_Btn_17,
    }

    [SerializeField] private TMP_Text _achievementText;
    [SerializeField] private TMP_Text _achievementDescriptionText;
    private int _isClear = -1;
    private Color _disableColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);
    private Color _enableColor = new Color(1f, 1f, 1f);

    private void Awake()
    {
        Bind<GameObject>(typeof(Buttons));
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Buttons));

        Addressables.LoadAssetAsync<SpriteAtlas>("SA_Achievement").Completed += SetAllAchievementImage;

        for (int i = 0; i < 18; i++)
        {
            Get<GameObject>(i).SetActive(false);
        }

        for (int i = 0; i < FlowManager.Instance.MaxAchievementCount; i++)
        {
            Get<GameObject>(i).SetActive(true);
            Button button = Get<Button>(i);
            int c = i;
            button.onClick.AddListener(() => UI_Acheivement_Btn(c));
        }

        AddAllTextToLocalizeStringEvent();
        LoadAchievementBit();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        LoadAchievementBit();
    }

    private void LoadAchievementBit()
    {
        int isClear = FlowManager.Instance.ClearBit;
        if(_isClear != isClear)
        {
            _isClear = isClear;

            for (int i = 0; i < FlowManager.Instance.MaxAchievementCount; i++)
            {
                int temp = 1 << i;

                Image image = Get<Image>(i);
                if ((_isClear & temp) == 0)
                {
                    if (image != null)
                    {
                        image.color = _disableColor;
                    }

                }
                else
                {
                    if (image != null)
                    {
                        image.color = _enableColor;
                    }
                }
            }
        }
    }

    public void UI_Acheivement_Btn(int index)
    {
        Locale currentLanguage = LocalizationSettings.SelectedLocale;

        Debug.Log(index.ToString());

        int bit = 1 << index;
        if((bit & _isClear) == 0)
        {
            _achievementText.text = LocalizationSettings.StringDatabase.GetLocalizedString("AchievementTable", "NotClear", currentLanguage);
            _achievementDescriptionText.text = LocalizationSettings.StringDatabase.GetLocalizedString("AchievementTable", "NotClear_Description", currentLanguage);
        }
        else
        {
            _achievementText.text = LocalizationSettings.StringDatabase.GetLocalizedString("AchievementTable", index.ToString(), currentLanguage);
            _achievementDescriptionText.text = LocalizationSettings.StringDatabase.GetLocalizedString("AchievementTable", index.ToString() + "_Description", currentLanguage);
        }
    }

    private void SetAllAchievementImage(AsyncOperationHandle<SpriteAtlas> handle)
    {
        SpriteAtlas spriteAtlas = handle.Result;
        for(int i = 0; i < FlowManager.Instance.MaxAchievementCount; i++)
        {
            Get<Image>(i).sprite = spriteAtlas.GetSprite(i.ToString());
        }

        //Addressables.Release(handle);
    }

}
