using UnityEngine;
using TMPro;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class UI_Chatting : UI_Popup
{
    private InteractionableNPC _currentNPC = null;
    private NPCController _currentController = null;

    public TMP_InputField _inputField;
    public TMP_Text _npcText;
    public Button _sendButton;

    private OpenAIAPI _api;
    private List<ChatMessage> _messageList;

    private void Awake()
    {

    }

    private void Start()
    {
        _api = new OpenAIAPI("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");
        _sendButton.onClick.AddListener(() => GetResponse());
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if(_currentNPC != null)
        {
            _currentNPC.FinishInteraction();
        }
    }

    /// <summary>
    /// NPC ����
    /// </summary>
    public void UpdateNPC(InteractionableNPC newNPC)
    {
        _currentNPC = newNPC;
        _currentController = newNPC.transform.GetComponent<NPCController>();
        StartConversation();
    }

    private void StartConversation()
    {
        _messageList = new List<ChatMessage>
        {
            new ChatMessage(ChatMessageRole.System, "#���� ���� �� ������ �÷��̾ A �κ����� �ְŸ� �����ϰ� �ɹ��ϸ� ��ǿ� ���� ������ ������ �����Դϴ�. ����� ���� �� �� �κ����� ���� ������� ���� �ɹ��� ���ϸ� �˴� ��. ����� �÷��̾ �Է��� �� ���� ��ٸ����� #����� ���� �̸�=���� ���� = �������, �ǹ���, ������ ������� ����. �󼼼��� =����� ���� �� A �κ��Դϴ�. B ��� NPC�� �Ƶ��� �� �� �� �Ƶ��� ����� �н��� �κ��� ����Դϴ�. �� �н��� ���� ���� �� �ڽ��� ���� B�� �Ƶ��̶� �����ϰ� �ֽ��ϴ�. #����� ���� ����� ���� =�� ����� ������ ���(����) �Դϴ�. ���ڴ� �н��� �� ���� ������ ���� �ƹ����� ������ ��� ���Ͽ� ��ư ���ܰ� ����� ������ �ʰԵǾ����ϴ�. �� �� ���ڴ� ������ ���� �ƹ����� �������� �� �ƹ����� �ڵ������� �����泪 ��� �����Ĺ��� ȭ���� �ֹ� �Ͽ� ���ϴ�. �� �� ���� �Ƴ� �η� �� �湮 �Ͽ�����, �߸���� �޾Ҵ� �� ���� ��￡ �̿��� ���ڴ� �ڽ��� ������ ab123 �̶�� ������ ��� �Ͽ� -�ΰ�����ڵ���\"�� ������ �� �η��� �� ��Ȳ�� ���󵷳��� �� �� ������ bcd �๰�� ������ �����Ͽ� ����óġ�� �Ͽ��� ������Ŭ �۵���׽��ϴ�. ����� ������ ���� �̸� ������� ����糪 ���Ÿ� �Ѿ� �ξ��� ġ�緮�� ���ԵǱ� �Ǿ� �ƹ����� ����� �̸��� �˴ϴ�. �� �� ������ �������� �ɷ��� ���Ͽ� �������� �����մϴ�. #����� ���� ó�� ����� ���������� ������ �ڹ����� �ʽ��ϴ�. if ����� ������ ���õ� ����� �ܼ��� �Բ� �������� ��� \"[�ý��� ������]\" ��� �޽����� �Բ� �������� �մϴ�. else if ����� ���������� ���� ���� ����� ������� ���� �����ų� ������ �޾Ƶ����� �ʴ� ����� �ϼ��� else: �� ���� ��쿡�� ����� ������ ���� ����ϼ���.")
        };

        _inputField.text = "";

        Locale currentLanguage = LocalizationSettings.SelectedLocale;
        string startString = LocalizationSettings.StringDatabase.GetLocalizedString("NPC", _currentNPC.ID.ToString() + "_Hello", currentLanguage);

        _npcText.text = startString;
    }

    private async void GetResponse()
    {
        if (_inputField.text.Length < 1)
        {
            return;
        }
        //��ư Disable
        _sendButton.enabled = false;

        //���� �޼����� inputField��
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = _inputField.text;
        if (userMessage.Content.Length > 100)
        {
            userMessage.Content = userMessage.Content.Substring(0, 100);
        }
        Debug.Log(string.Format("{0} : {1}", userMessage.Role, userMessage.Content));

        //list�� �޼��� �߰�
        _messageList.Add(userMessage);

        //textField�� userMessageǥ�� 
        _npcText.text = string.Format("You: {0}", userMessage.Content);

        //inputField �ʱ�ȭ
        _inputField.text = "";

        // ��ü ä���� openAI �����������Ͽ� ���� �޽���(����)�� ����������
        var chatResult = await _api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 200,
            Messages = _messageList
        });

        //���� ��������
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        //������ message����Ʈ�� �߰�
        _messageList.Add(responseMessage);

        //textField�� ���信 ���� Update
        _npcText.text = string.Format("You: {0}\n\nChatGPT:\n{1}", userMessage.Content, responseMessage.Content);

        //Okbtn�ٽ� Ȱ��ȭ
        _sendButton.enabled = true;

    }
}