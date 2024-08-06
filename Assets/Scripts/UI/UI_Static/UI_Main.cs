using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class UI_Main : UI_Static
{
    [SerializeField] private UI_Static _hudUI;
    [SerializeField] private AssetReference _logoAssetRef;
    public GameObject _shakingCamera = null;

    public Vector3 _mainScenePlayerPosition = Vector3.zero;

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

        FlowManager.Instance.Player.GetComponent<PlayerMovementAdvanced>().DeactiveInput();
        UIManager.Instance.ChangeStaticUI(this);

        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Buttons));
        
        AddAllTextToLocalizeStringEvent();
        BindAllButtonToOnClickFunc();

        _mainScenePlayerPosition = FlowManager.Instance.Player.transform.position;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (_shakingCamera == null) _shakingCamera = GameObject.FindGameObjectWithTag("ShakingCamera");
        if (_shakingCamera != null) _shakingCamera.SetActive(true);
        if (_shakingCamera != null) _shakingCamera.GetComponent<CinemachineCamera>().Priority = 110;

        GameObject obj = FlowManager.Instance.Player;
        PlayerMovementAdvanced adv = obj.GetComponent<PlayerMovementAdvanced>();
        adv.DeactiveInput();
        if (_mainScenePlayerPosition != Vector3.zero)
        {
            FlowManager.Instance.Player.transform.position = _mainScenePlayerPosition;
        }
    }

    public void UI_Main_Btn_1()
    {
        UIManager.Instance.ChangeStaticUI(_hudUI);

        _shakingCamera.GetComponent<CinemachineCamera>().Priority = 0;
        Invoke("DisableShakingCamera", 1f);
        FlowManager.Instance.Player.GetComponent<PlayerMovementAdvanced>().ActiveInput();
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

    private void DisableShakingCamera()
    {
        _shakingCamera.SetActive(false);
    }
}
