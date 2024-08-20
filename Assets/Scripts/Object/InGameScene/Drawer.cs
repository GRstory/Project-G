using DG.Tweening;
using UnityEngine;

public class Drawer : InteractionableObject
{
    Vector3 _originalPosition;
    Vector3 _openedPosition;
    [SerializeField]private float _openedLength = 0.3f;

    protected override void Awake()
    {
        base.Awake();

        _originalPosition = transform.position;
        _openedPosition = _originalPosition + transform.forward * _openedLength;
    }

    protected override void OverInteractionCount()
    {
        
    }

    protected override void ProgressInteraction()
    {
        if (_interactionCount % 2 == 1)
        {
            transform.DOMove(_openedPosition, 1f).SetEase(Ease.InQuart);
        }
        else
        {
            transform.DOMove(_originalPosition, 1f).SetEase(Ease.InQuart);
        }
    }
}
