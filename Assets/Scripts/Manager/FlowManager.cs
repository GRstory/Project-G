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
    /// CollectableItem�� Start() ���� ��ųʸ��� ������ ����մϴ�.
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
    /// ��ųʸ��� �������� �ִ��� Id�� �˻��մϴ�.
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
    /// ��ųʸ��� ��ϵ� �������� ������ ������ ����Ʈ�� �߰��մϴ�.
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
    /// ������ ������ ����Ʈ�� �ʱ�ȭ �մϴ�.
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
    /// �޼��� �������� �ε����� �޾� ��Ʈ������ �����մϴ�.
    /// </summary>
    public void ClearAchievement(int index)
    {
        int temp = 1 << index;
        _saveSetting.achievement = _saveSetting.achievement | temp;
        SaveManager.Instance.Save(_saveSetting, _saveSettingPath);
    }

    /// <summary>
    /// Ŭ������ ���� �ε����� ������ �޾� �����մϴ�.
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
