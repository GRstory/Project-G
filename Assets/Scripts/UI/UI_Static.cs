using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UI_Static : UI_Base
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected virtual void Start()
    {
        UIManager.Instance.ChangeStaticUI(this);
    }
}
