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
    [SerializeField] private GameObject _player = null;
    [SerializeField] private Dictionary<int, CollectableItem> _collectableItemDict;
    [SerializeField] private List<CollectableItem> _currentCollectableItemList;

    public GameObject Player {  get { return _player; }  set { _player = value; } }

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
}
