using UnityEngine;

public class UI_Debug : MonoBehaviour
{
    public int _achievementinput;

    public void SetAchievementBit(string input)
    {
        _achievementinput = int.Parse(input);
    }

    public void ClearAchievement()
    {
        FlowManager.Instance.ClearAchievement(_achievementinput);
    }

    public void UnClearAchivement()
    {
        int bit = FlowManager.Instance.ClearBit;
        bit = bit & ~(1 << _achievementinput);
        FlowManager.Instance.ClearBit = bit;
    }

    public void UnClearAllAchivement()
    {
        FlowManager.Instance.ClearBit = 0;
    }

    public void SaveGame()
    {
        FlowManager.Instance.SaveGame();
    }

    public void LoadGame()
    {
        FlowManager.Instance.LoadGame();
    }

    public void MoveSpawn()
    {
        FlowManager.Instance.Player.transform.position = Vector3.zero;
    }

}
