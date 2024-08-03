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
}
