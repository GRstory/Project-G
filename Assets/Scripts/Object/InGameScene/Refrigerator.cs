using DG.Tweening;
using UnityEngine;

public class InteractionDoor : InteractionableObject
{
    [SerializeField] bool _isOpenRight = true;
    [SerializeField] float _openAngle = 160f;

    Vector3 _originalRotate;
    Vector3 _openedRotate;

    protected override void Awake()
    {
        _originalRotate = transform.rotation.eulerAngles;
        if( _isOpenRight )
        {
            _openedRotate = _originalRotate - new Vector3(0, _openAngle, 0);
        }
        else
        {
            _openedRotate = _originalRotate - new Vector3(0, -_openAngle, 0);
        }
    }
    protected override void OverInteractionCount()
    {
        
    }

    protected override void ProgressInteraction()
    {
        if(_interactionCount % 2 == 1)
        {
            transform.DORotate(_openedRotate, 1f).SetEase(Ease.OutBack);
        }
        else
        {
            transform.DORotate(_originalRotate, 1f).SetEase(Ease.InQuart);
        }
    }
}
