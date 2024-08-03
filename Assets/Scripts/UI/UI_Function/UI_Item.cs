using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    public int _index;
    public int _itemId;
    public string _positionKey;
    public Image _image;
    public TMP_Text _text;
    public Button _button;

    public TMP_Text _itemPositionText;
    private Coroutine _resetCoroutine;

    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        Locale currentLanguage = LocalizationSettings.SelectedLocale;
        _itemPositionText.text = LocalizationSettings.StringDatabase.GetLocalizedString("WorldMapPosition", _positionKey, currentLanguage);

        if(_resetCoroutine != null)
        {
            StopCoroutine( _resetCoroutine );
        }
        _resetCoroutine = StartCoroutine(ResetItemPositionTextCoroutine());
    }

    private IEnumerator ResetItemPositionTextCoroutine()
    {
        yield return new WaitForSeconds(3f);
        Locale currentLanguage = LocalizationSettings.SelectedLocale;
        _itemPositionText.text = LocalizationSettings.StringDatabase.GetLocalizedString("UI Table", "UI_Inventory_ItemPositionText", currentLanguage);
    }
}
