using Unity.Cinemachine;
using UnityEngine;

public class InteractionableEvidence : Interactionable
{
    [SerializeField] private UI_DetailView _detailViewUI;
    [SerializeField] public GameObject _prefap = null;
    [SerializeField] public float _cameraPosition = 1f;

    protected override void Start()
    {
        base.Start();
        _detailViewUI = (UIManager.Instance.DetailViewUI is UI_Popup) ? _detailViewUI = (UI_DetailView)UIManager.Instance.DetailViewUI : null;
    }

    protected override void Interaction(Transform fromTransform)
    {
        base.Interaction(fromTransform);

        UIManager.Instance.OnActivePopupUI(_detailViewUI);
        _detailViewUI.UpdateObject(this, _cameraPosition);
    }

    public override void FinishInteraction()
    {
        
    }
}
