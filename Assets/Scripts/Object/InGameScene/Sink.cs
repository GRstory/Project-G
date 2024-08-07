using DG.Tweening;
using UnityEngine;

public class Sink : InteractionableObject
{
    [SerializeField] private Transform _doorL;
    [SerializeField] private Transform _doorR;

    private Vector3 _doorLOriginalRotate;
    private Vector3 _doorLOpenedRotate;
    private Vector3 _doorROriginalRotate;
    private Vector3 _doorROpenedRotate;

    protected override void Awake()
    {
        _doorLOriginalRotate = _doorL.transform.rotation.eulerAngles;
        _doorROriginalRotate = _doorR.transform.rotation.eulerAngles;
        _doorLOpenedRotate = _doorLOriginalRotate + new Vector3(0, 110, 0);
        _doorROpenedRotate = _doorROriginalRotate + new Vector3(0, -110, 0);
    }

    protected override void OverInteractionCount()
    {
        
    }

    protected override void ProgressInteraction()
    {
        if (_interactionCount % 2 == 1)
        {
            _doorL.DORotate(_doorLOpenedRotate, 1f).SetEase(Ease.OutBack);
            _doorR.DORotate(_doorROpenedRotate, 1f).SetEase(Ease.OutBack);
        }
        else
        {
            _doorL.transform.DORotate(_doorLOriginalRotate, 1f).SetEase(Ease.InQuart);
            _doorR.transform.DORotate(_doorROriginalRotate, 1f).SetEase(Ease.InQuart);
        }
    }
}
