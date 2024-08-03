using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : SingletonMonobehavior<AchievementManager> 
{
    [SerializeField] private int _maxAchievementCount = 18;
    [SerializeField] private int _clearBit = 0;

    public int MaxAchievementCount {  get { return _maxAchievementCount; } }

    protected override void Awake()
    {
        base.Awake();
    }

    public int GetClearBit()
    {
        return _clearBit;
    }

    public void ClearAchievement(int index)
    {
        int temp = 1 << index;
        _clearBit = _clearBit | temp;
    }
}
