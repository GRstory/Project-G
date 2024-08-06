using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveSetting
{
    public float mouseSpeed = 100f;
    public float moveSpeed = 3f;

    public int clearLevel = 0;
    public int[] clearStars = new int[5];

    public int achievement = 0;

    public SaveSetting()
    {

    }

    public SaveSetting(float mouseSpeed, float moveSpeed, int clearLevel, int[] clearStars, int achievement)
    {
        this.mouseSpeed = mouseSpeed;
        this.moveSpeed = moveSpeed;
        this.clearLevel = clearLevel;
        this.clearStars = clearStars;
        this.achievement = achievement;
    }
}
