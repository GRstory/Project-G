using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CollectableItem
{
    public CollectableItem(int id, string position, Sprite sprite)
    {
        this.id = id;
        this.position = position;
        this.sprite = sprite;
    }

    public int id;
    public string position;
    public Sprite sprite;
}


public class FlowManager : SingletonMonobehavior<FlowManager>
{
    //Save
    [SerializeField] private SaveSetting _saveSetting = new SaveSetting();
    private string _saveSettingPath = "save.bin";

    //Player
    [SerializeField] private GameObject _player = null;

    //Inventory
    [SerializeField] private Dictionary<int, CollectableItem> _collectableItemDict;
    [SerializeField] private List<CollectableItem> _currentCollectableItemList;

    //Achievement
    [SerializeField] private int _maxAchievementCount = 6;

    //SceneController
    [SerializeField] private SceneController _sceneController;

    public int MaxAchievementCount { get { return _maxAchievementCount; } }
    public GameObject Player { get { return _player; } set { _player = value; } }
    public SaveSetting SaveSetting { get { return _saveSetting; } set { _saveSetting = value; } }
    public int ClearBit { get { return _saveSetting.achievement; } set { _saveSetting.achievement = value; } }
    public SceneController SceneController { get { return _sceneController; } set { _sceneController = value; } }

    protected override void Awake()
    {
        base.Awake();
        _collectableItemDict = new Dictionary<int, CollectableItem>();
        _currentCollectableItemList = new List<CollectableItem>();
    }

    private void Start()
    {
        
    }

    /// <summary>
    /// CollectableItem은 Start() 에서 딕셔너리에 스스로 등록합니다.
    /// </summary>
    public void RegisterCollectableItem(CollectableItem item)
    {
        if (_collectableItemDict.TryGetValue(item.id, out CollectableItem outItem))
        {
            return;
        }
        else
        {
            _collectableItemDict.Add(item.id, item);
        }
    }

    /// <summary>
    /// 딕셔너리에 아이템이 있는지 Id로 검사합니다.
    /// </summary>
    public bool IsHaveCollectableItem(int id)
    {
        CollectableItem? item = _collectableItemDict[id];
        if(item == null) return false;

        if (_currentCollectableItemList.Contains(item.Value))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 딕셔너리에 등록된 아이템을 수집한 아이템 리스트에 추가합니다.
    /// </summary>
    public bool TakeCollectableItem(int id)
    {
        if(_collectableItemDict.TryGetValue(id, out CollectableItem item))
        {
            _currentCollectableItemList.Add(item);
            return true;
        }
        else
        {
            EventHandler.CallAddAlarmMessageEvent($"Error to take Collectable Item: {id}");
            return false;
        }
    }

    /// <summary>
    /// 수집한 아이템 리스트를 초기화 합니다.
    /// </summary>
    public void ClearCollectableItem()
    {
        _currentCollectableItemList.Clear();
    }

    public List<CollectableItem> GetCollectableItemList()
    {
        return _currentCollectableItemList;
    }

    /// <summary>
    /// 달성한 도전과제 인덱스를 받아 비트연산을 시행합니다.
    /// </summary>
    public void ClearAchievement(int index)
    {
        int temp = 1 << index;
        _saveSetting.achievement = _saveSetting.achievement | temp;
        SaveManager.Instance.Save(_saveSetting, _saveSettingPath);
    }

    /// <summary>
    /// 클리어한 레벨 인덱스와 별점을 받아 수정합니다.
    /// </summary>
    public void ClearLevel(int level, int star)
    {
        _saveSetting.clearLevel = level;

        if(_saveSetting.clearStars.Length >= level)
        {
            if (_saveSetting.clearStars[level] < star)
            {
                _saveSetting.clearStars[level] = star;
            }
        }

        SaveManager.Instance.Save(_saveSetting, _saveSettingPath);
    }

    public void LoadGame()
    {
        _saveSetting = SaveManager.Instance.Load(_saveSettingPath);
    }

    public void SaveGame()
    {
        SaveManager.Instance.Save(_saveSetting, _saveSettingPath);
    }
}
