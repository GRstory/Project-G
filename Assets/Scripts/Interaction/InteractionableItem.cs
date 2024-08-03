using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class InteractionableItem : Interactionable
{
    [SerializeField] Sprite _itemSprite;
    private InteractionController _interactionController;
    [SerializeField] private string _itemPositionKey = "";
    static private string _alarmMessage1Key = "Alarm_TakeCollectableItem_1";
    static private string _alarmMessage2Key = "Alarm_TakeCollectableItem_2";

    public string ItemPositionKey {get { return _itemPositionKey;} set { _itemPositionKey = value; } }

    private void Awake()
    {
        _interactionType = GameEnum.InteractionType.Collectable;
    }

    protected override void Start()
    {
        base.Start();

        _interactionController = GameObject.FindGameObjectWithTag("PlayerCamera").transform.GetComponentInChildren<InteractionController>();
        
        StartCoroutine(RegisterCollectableItemToFlowManger());
    }

    protected override void Interaction(Transform fromTransform)
    {
        base.Interaction(fromTransform);

        if(FlowManager.Instance.TakeCollectableItem(_id))
        {
            Locale currentLanguage = LocalizationSettings.SelectedLocale;
            string itemName = LocalizationSettings.StringDatabase.GetLocalizedString("InteractionableObject", _id.ToString(), currentLanguage);
            string alarmMessage1 = LocalizationSettings.StringDatabase.GetLocalizedString("Misc", _alarmMessage1Key, currentLanguage);
            string alarmMessage2 = LocalizationSettings.StringDatabase.GetLocalizedString("Misc", _alarmMessage2Key, currentLanguage);

            EventHandler.CallAddAlarmMessageEvent(alarmMessage1 + itemName + alarmMessage2);

            _interactionController.RemoveInteractionableObjectList(this);
            gameObject.SetActive(false);
        }
    }

    public override void FinishInteraction()
    {

    }

    private IEnumerator RegisterCollectableItemToFlowManger()
    {
        yield return new WaitForSeconds(1);

        FlowManager.Instance.RegisterCollectableItem(new CollectableItem(_id, _itemPositionKey, _itemSprite));
    }
}
