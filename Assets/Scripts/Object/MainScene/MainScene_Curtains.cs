using System;
using UnityEngine;

public class MainScene_Curtains : MonoBehaviour
{
    GameObject _child0;
    GameObject _child1;

    private void Start()
    {
        _child0 = transform.GetChild(0).gameObject;
        _child1 = transform.GetChild(1).gameObject;

        DateTime time = DateTime.Now;
        if (time.Hour > 18 || time.Hour < 7)
        {
            _child0.SetActive(false);
            _child1.SetActive(true);
        }
        else
        {
            _child0.SetActive(true);
            _child1.SetActive(false);
        }
    }
}
