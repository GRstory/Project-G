using DG.Tweening;
using UnityEngine;

public class Door1 : InteractionableObject
{
    protected override void ProgressInteraction()
    {
        Vector3 toRotate = transform.rotation.eulerAngles - new Vector3(0, -100, 0);

        transform.DORotate(toRotate, 2f).SetEase(Ease.OutBack);
    }
}
