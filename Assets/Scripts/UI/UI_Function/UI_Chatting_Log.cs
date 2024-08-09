using TMPro;
using UnityEngine;

public class UI_Chatting_Log : MonoBehaviour
{
    [SerializeField] private TMP_Text _sendText;
    [SerializeField] private TMP_Text _rectText;

    public void SetText(string sendText, string rectText)
    {
        if(_sendText != null || _rectText != null)
        {
            _sendText.text = sendText;
            _rectText.text = rectText;
        }
    }
}
