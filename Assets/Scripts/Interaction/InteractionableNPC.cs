using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionableNPC : Interactionable
{
    [SerializeField] private UI_Chatting _chattingUI;
    [SerializeField] public GameObject _prefap = null;
    private CinemachineCamera _npcCamera = null;

    protected override void Start()
    {
        base.Start();
        _chattingUI = (UIManager.Instance.ChattingUI is UI_Chatting) ? (UI_Chatting)UIManager.Instance.ChattingUI : null;
        _npcCamera = GetComponentInChildren<CinemachineCamera>();
        _npcCamera.Priority = 110; 
        _npcCamera.gameObject.SetActive(false);
    }

    protected override void Interaction(Transform fromTransform)
    {
        base.Interaction(fromTransform);

        UIManager.Instance.OnActivePopupUI(_chattingUI);
        _chattingUI.UpdateNPC(this);
        _npcCamera.gameObject.SetActive(true);
    }

    public override void FinishInteraction()
    {
        _npcCamera.gameObject.SetActive(false);
    }
}
