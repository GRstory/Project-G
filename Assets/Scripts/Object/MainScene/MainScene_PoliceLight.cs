using System;
using System.Collections;
using UnityEngine;

public class MainScene_PoliceLight : MonoBehaviour
{
    Light _light;
    [SerializeField] GameObject _directionalLightObject;

    private void Start()
    {
        _light = GetComponent<Light>();
        StartCoroutine(PoliceLightCoroutine());

        /*DateTime time = DateTime.Now;
        if(time.Hour > 18 || time.Hour < 7) _directionalLightObject.SetActive(false);
        else _directionalLightObject.SetActive(true);*/
    }

    private IEnumerator PoliceLightCoroutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            _light.color = Color.red;
            yield return new WaitForSecondsRealtime(1.5f);
            _light.color = Color.blue;
        }
    }
}
