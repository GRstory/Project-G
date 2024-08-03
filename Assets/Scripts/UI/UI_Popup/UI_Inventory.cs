using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class UI_Inventory : UI_Popup
{
    private UI_Item[] _itemArray;
    [SerializeField] TMP_Text _itemPositionText;
    [SerializeField] GameObject _zeroItemTextObject;

    private void Awake()
    {
        _itemArray = GetComponentsInChildren<UI_Item>(true);
        for(int i = 0;  i < _itemArray.Length; i++)
        {
            _itemArray[i]._index = i;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        List<CollectableItem> itemList = FlowManager.Instance.GetCollectableItemList();
        Locale currentLanguage = LocalizationSettings.SelectedLocale;
        _itemPositionText.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI Table", "UI_Inventory_ItemPositionText", currentLanguage);

        if(itemList.Count == 0 )
        {
            _zeroItemTextObject.SetActive(true);
        }
        else
        {
            _zeroItemTextObject.SetActive(false);

            for (int i = 0; i < itemList.Count; i++)
            {
                if (i > _itemArray.Length) return;

                UI_Item item = _itemArray[i];
                CollectableItem collectableItem = itemList[i];

                item.gameObject.SetActive(true);
                item._itemId = collectableItem.id;
                item._positionKey = collectableItem.position;
                item._image.sprite = collectableItem.sprite;
                item._text.text = LocalizationSettings.StringDatabase.GetLocalizedString("InteractionableObject", collectableItem.id.ToString(), currentLanguage);
            }
        }
        

        FlowManager.Instance.Player.GetComponent<PlayerController>().DeactiveInput();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        for (int i = 0; i < _itemArray.Length; i++)
        {
            _itemArray[i].gameObject.SetActive(false);
        }
        FlowManager.Instance.Player.GetComponent<PlayerController>().ActiveInput();
        FlowManager.Instance.Player.GetComponent<PlayerController>()._isInventory = false;
    }
}
