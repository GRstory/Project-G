using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : SingletonMonobehavior<UIManager>
{
    private Stack<UI_Popup> _popupUIStack;

    [SerializeField] private UI_Static _mainUI = null;
    [SerializeField] private UI_Static _hudUI = null;
    [SerializeField] private UI_Popup _pauseUI = null;
    [SerializeField] private UI_Popup _chattingUI = null;
    [SerializeField] private UI_Popup _detailViewUI = null;
    [SerializeField] private UI_Popup _inventoryUI = null;
    [SerializeField] private UI_Popup _helpUI = null;
    [SerializeField] private UI_Popup _achievementUI = null;

    [SerializeField] private UI_Static _staticUI;

    public UI_Static MainUI { get { return _mainUI; } }
    public UI_Static HUDUI { get { return _hudUI; } }
    public UI_Popup PauseUI { get { return _pauseUI; } }
    public UI_Popup ChattingUI { get { return _chattingUI; } }
    public UI_Popup DetailViewUI {  get { return _detailViewUI; } }
    public UI_Popup InventoryUI { get { return _inventoryUI; } }
    public UI_Popup HelpUI { get { return _helpUI; } }
    public UI_Popup AchievementUI { get { return _achievementUI; } }
    public UI_Static StaticUI { get { return _staticUI; } set { _staticUI = value; } }

    protected override void Awake()
    {
        base.Awake();
        _popupUIStack = new Stack<UI_Popup>();
    }

    private void Start()
    {
        
    }

    public void ChangeStaticUI(UI_Static newUI)
    {
        if(_staticUI != null)
        {
            _staticUI.gameObject.SetActive(false);
        }

        _staticUI = newUI;
        _staticUI.gameObject.SetActive(true);

        while(_popupUIStack.Count > 0)
        {
            OnDeactivePopupUI();
        }
    }

    /// <summary>
    /// PopupUI를 활성화 합니다.
    /// </summary>
    public void OnActivePopupUI(UI_Popup popupUI)
    {
        if(popupUI == null) return;

        if (_popupUIStack.Count > 0)
        {
            _popupUIStack.Peek().gameObject.SetActive(false);
        }

        if (_staticUI != null)
        {
            _staticUI.gameObject.SetActive(false);
        }

        _popupUIStack.Push(popupUI);
        popupUI.gameObject.SetActive(true);
    }

    /// <summary>
    /// PopupUI를 비활성화 합니다. 이미 비활성화 되었을 경우에는 Pause 메뉴를 실행합니다.
    /// </summary>
    public void OnDeactivePopupUI()
    {
        if (_popupUIStack.Count > 0)
        {
            UI_Popup topUI = _popupUIStack.Pop();
            topUI.gameObject.SetActive(false);

            if (_popupUIStack.Count == 0)
            {
                if (_staticUI != null)
                {
                    _staticUI.gameObject.SetActive(true);
                }
            }
            else
            {
                UI_Popup newTopUI = _popupUIStack.Peek();
                newTopUI.gameObject.SetActive(true);
            }

        }
        else
        {
            UI_Main mainUI = _staticUI as UI_Main;
            if (mainUI == null)
            {
                OnActivePopupUI(_pauseUI);
            }
        }
    }

    public bool IsPopupUIActive()
    {
        if (_popupUIStack.Count > 0)
            return true;
        else return false;
    }

    public void CloseAllPopupUI()
    {
        while(_popupUIStack.Count > 0)
        {
            OnDeactivePopupUI();
        }
    }
}
