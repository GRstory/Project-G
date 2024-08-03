using System.Collections;
using UnityEngine;

public class TestInteractionableObject : InteractionableObject
{
    public GameObject _doorL;
    public GameObject _doorR;
    public GameObject _doorLPivot;
    public GameObject _doorRPivot;
    public GameObject _drawer;

    private void Awake()
    {
        _doorL = transform.Find("DoorL").gameObject;
        _doorR = transform.Find("DoorR").gameObject;
        _doorLPivot = transform.Find("DoorLPivot").gameObject;
        _doorRPivot = transform.Find("DoorRPivot").gameObject;
        _drawer = transform.Find("Drawer").gameObject;
    }

    protected override void Interaction(Transform fromTransform)
    {
        base.Interaction(fromTransform);
    }

    protected override void OverInteractionCount()
    {
        EventHandler.CallAddAlarmMessageEvent("TESTInteractionableObject : 이미 사용했습니다!");
    }

    protected override void ProgressInteraction()
    {
        StartCoroutine(InteractionCoroutine());
    }

    IEnumerator InteractionCoroutine()
    {
        float sec = 0f;
        float delta = 0f;
        while(sec <= 2f)
        {
            float step = (90 / 2f) * Time.deltaTime;
            delta += step;
            _doorL.transform.RotateAround(_doorLPivot.transform.position, Vector3.down, -step);
            _doorR.transform.RotateAround(_doorRPivot.transform.position, Vector3.down, step);
            sec += Time.deltaTime;

            yield return null;
        }

        _doorL.transform.rotation = Quaternion.Euler(0, -90, 0);
        _doorR.transform.rotation = Quaternion.Euler(0, 90, 0);

        sec = 0f;
        delta = 0f;
        while (delta <= 0.7f)
        {
            float step = (1f / 1f) * Time.deltaTime;
            delta += step;
            _drawer.transform.position += new Vector3(0, 0, -step);

            yield return null;
        }
    }
}
