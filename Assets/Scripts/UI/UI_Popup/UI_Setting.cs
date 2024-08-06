using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class UI_Setting : UI_Popup
{
    [SerializeField] private List<Button> _tabButtonList;
    [SerializeField] private List<GameObject> _tabList;

    private GameEnum.ScreenMode _screenMode = GameEnum.ScreenMode.FullScreenWindow;
    private int _screenResolutionWidth = 1920;
    private int _screenResolutionHeight = 1080;
    private int _screenRefreshlate = 144;

    [SerializeField] private TMP_Dropdown _T1_S2;
    [SerializeField] private Slider _T2_S1;
    [SerializeField] private Slider _T2_S2;
    [SerializeField] private Slider _T2_S3;


    private void Start()
    {
        InitDropDown_T1_S2();
        InitTab2();
        _T2_S1.onValueChanged.AddListener(OnMoveSlider_T2_S1);
        _T2_S2.onValueChanged.AddListener(OnMoveSlider_T2_S2);
        _T2_S3.onValueChanged.AddListener(OnMoveSlider_T2_S3);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        FlowManager.Instance.Player.GetComponent<PlayerMovementAdvanced>().DeactiveInput();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        FlowManager.Instance.Player.GetComponent<PlayerMovementAdvanced>().ActiveInput();
    }

    public void OnTabButtonClick(int index)
    {
        for(int i = 0; i < _tabList.Count; i++)
        {
            _tabList[i].gameObject.SetActive(false);
        }
        _tabList[index].gameObject.SetActive(true);

        for (int i = 0; i < _tabButtonList.Count; i++)
        {
            _tabButtonList[i].transform.GetComponentInChildren<TMP_Text>().color = Color.black;

            ColorBlock colorBlock = _tabButtonList[i].colors;
            colorBlock.normalColor = new Color(217f/255f, 194f/255f, 167f/255f);
            _tabButtonList[i].colors = colorBlock;
        }
        _tabButtonList[index].transform.GetComponentInChildren<TMP_Text>().color = Color.white;

        ColorBlock selectColorBlock = _tabButtonList[index].colors;
        selectColorBlock.normalColor = new Color(130f / 255f, 116f / 255f, 100f / 255f);
        _tabButtonList[index].colors = selectColorBlock;
    }

    public void OnClickButton_T1_S1(int index)
    {
        switch (index)
        {
            case 0:
                _screenMode = ScreenMode.FullScreenWindow;
                break;
            case 1:
                _screenMode = ScreenMode.Windowed;
                break;
        }
        Screen.SetResolution(_screenResolutionWidth, _screenResolutionHeight, (FullScreenMode)_screenMode);
    }

    private void InitDropDown_T1_S2()
    {
        _T1_S2.options.Clear();
        _T1_S2.options.Add(new TMP_Dropdown.OptionData("1600 * 900"));
        _T1_S2.options.Add(new TMP_Dropdown.OptionData("1920 * 1080"));
        _T1_S2.options.Add(new TMP_Dropdown.OptionData("2560 * 1080"));
        _T1_S2.options.Add(new TMP_Dropdown.OptionData("3840 * 1080"));
        _T1_S2.options.Add(new TMP_Dropdown.OptionData("2560 * 1440"));
        _T1_S2.options.Add(new TMP_Dropdown.OptionData("3440 * 1440"));
        _T1_S2.options.Add(new TMP_Dropdown.OptionData("5120 * 1440"));
        _T1_S2.options.Add(new TMP_Dropdown.OptionData("3840 * 2160"));
    }

    public void OnSelectDropDown_T1_S2()
    {
        switch(_T1_S2.value)
        {
            case 0:
                _screenResolutionWidth = 1600;
                _screenResolutionHeight = 900;
                break;
            case 1:
                _screenResolutionWidth = 1920;
                _screenResolutionHeight = 1080;
                break;
            case 2:
                _screenResolutionWidth = 2560;
                _screenResolutionHeight = 1080;
                break;
            case 3:
                _screenResolutionWidth = 3840;
                _screenResolutionHeight = 1080;
                break;
            case 4:
                _screenResolutionWidth = 2560;
                _screenResolutionHeight = 1440;
                break;
            case 5:
                _screenResolutionWidth = 3440;
                _screenResolutionHeight = 1440;
                break;
            case 6:
                _screenResolutionWidth = 5120;
                _screenResolutionHeight = 1440;
                break;
            case 7:
                _screenResolutionWidth = 3840;
                _screenResolutionHeight = 2160;
                break;
        }
        Screen.SetResolution(_screenResolutionWidth, _screenResolutionHeight, (FullScreenMode)_screenMode);
    }

    public void OnClickButton_T1_S3(int index)
    {
        switch(index)
        {
            case 0:
                _screenRefreshlate = 60;
                break;
            case 1:
                _screenRefreshlate = 120;
                break;
            case 2:
                _screenRefreshlate = 144;
                break;
        }
        Application.targetFrameRate = _screenRefreshlate;
    }

    public void OnClickButton_T1_S4(int index)
    {
        switch(index)
        {
            case 0:
                QualitySettings.vSyncCount = 1;
                break;
            case 1:
                QualitySettings.vSyncCount = 0;
                break;
        }
    }

    private void InitTab2()
    {
        _T2_S1.value = 100;
        _T2_S2.value = 100;
        _T2_S3.value = 100;
    }

    public void OnMoveSlider_T2_S1(float value)
    {
        
    }

    public void OnMoveSlider_T2_S2(float value)
    {
        
    }

    public void OnMoveSlider_T2_S3(float value)
    {
        
    }
    
}
