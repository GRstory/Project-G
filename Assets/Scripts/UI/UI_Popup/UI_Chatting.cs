using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

using TMPro;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public class UI_Chatting : UI_Popup
{

    enum Buttons
    {
        UI_Chatting_Btn_0,
        UI_Chatting_Btn_1,
        UI_Chatting_Btn_2,
        UI_Chatting_Btn_Send,
    }

    enum Texts
    {
        UI_Chatting_Text_Title,
        UI_Chatting_Text_SubTitle,
        UI_Chatting_Text_ESC,
        UI_Chatting_Text_0,
        UI_Chatting_Text_1,
        UI_Chatting_Text_2,
        UI_Chatting_Text_Send,
        UI_Chatting_Text_Info_0,
        UI_Chatting_Text_Info_1,
        UI_Chatting_Text_Info_2,
    }

    enum Tabs
    { 
        Tab0,
        Tab1,
        Tab2
    }


    private InteractionableNPC _currentNPC = null;
    private NPCController _currentController = null;

    public TMP_InputField _inputField;
    public TMP_Text _npcText;
    public Button _sendButton;

    private OpenAIAPI _api;
    private List<ChatMessage> _messageList;
    [SerializeField] private GameObject _logParent;
    [SerializeField] private AssetReference _assetRef;
    private GameObject _logPrefap;

    [SerializeField] private TMP_Text _info0_Data;
    [SerializeField] private TMP_Text _info1_Data;
    [SerializeField] private TMP_Text _info2_Data;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //Addressables.LoadAssetAsync<GameObject>(_assetRef).Completed += (handle) => { _logPrefap = handle.Result; };
        _api = new OpenAIAPI("");
        //������ ���ٸ� ������� �����ּ���... 
        //_sendButton.onClick.AddListener(() => GetResponse());

        Bind<GameObject>(typeof(Tabs));
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));

        AddAllTextToLocalizeStringEvent();
        BindAllButtonToOnClickFunc();

        Get<GameObject>(1).SetActive(false);
        Get<GameObject>(2).SetActive(false);
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

        Locale currentLanguage = LocalizationSettings.SelectedLocale;
        //string name = LocalizationSettings.StringDatabase.GetLocalizedString("InteractionableObject", _currentNPC.ID.ToString() + "_Hello", currentLanguage);
        _info0_Data.text = LocalizationSettings.StringDatabase.GetLocalizedString("NPC", _currentNPC.ID.ToString() + "_Gestalt", currentLanguage);
        _info1_Data.text = LocalizationSettings.StringDatabase.GetLocalizedString("NPC", _currentNPC.ID.ToString() + "_Persona", currentLanguage);
        _info2_Data.text = LocalizationSettings.StringDatabase.GetLocalizedString("NPC", _currentNPC.ID.ToString() + "_Flaw", currentLanguage);
    }

    private void StartConversation()
    {
        _messageList = new List<ChatMessage>
        {
            new ChatMessage(ChatMessageRole.System, "#���� ���� �� ������ �÷��̾ A �κ����� �ְŸ� �����ϰ� �ɹ��ϸ� ��ǿ� ���� ������ ������ �����Դϴ�. ����� ���� �� �� �κ����� ���� ������� ���� �ɹ��� ���ϸ� �˴� ��. ����� �÷��̾ �Է��� �� ���� ��ٸ����� #����� ���� �̸�=�ƶ� ���� = ������, �Ӹ����� �� ������ ����, ������ ������Դ� ��Ȥ�� �Ǵ��� ����. �󼼼��� =����� ���� �� A �κ��Դϴ�. B ��� NPC�� �Ƶ��� �� �� �� �Ƶ��� ����� �н��� �κ��� ����Դϴ�. �� �н��� ���� ���� �� �ڽ��� ���� B�� �Ƶ��̶� �����ϰ� �ֽ��ϴ�. #����� ���� ����� ���� =�� ����� ������ ���(�ƶ�) �Դϴ�. �ƶ�� �н��� �� ���� ������ ���� �ƹ����� ������ ��� ���Ͽ� ��ư ���ܰ� ����� ������ �ʰԵǾ����ϴ�. �� �� �ƶ�� ������ ���� �ƹ����� �������� �� �ƹ����� �ڵ������� �����泪 ��� �����Ĺ��� ȭ���� �ֹ� �Ͽ� ���ϴ�. �� �� ���� �Ƴ� �η� �� �湮 �Ͽ�����, �߸����� �޾Ҵ� �� ���� ��￡ �̿��� �ƶ�� �ڽ��� ������ ab123 �̶�� ������ ��� �Ͽ� -�ΰ�����ڵ���\"�� ������ �� �η��� �� ��Ȳ�� ���󵷳��� �� �� ������ bcd �๰�� ������ �����Ͽ� ����óġ�� �Ͽ��� ������Ŭ �۵���׽��ϴ�. ����� ������ ���� �̸� ������� ����糪 ���Ÿ� �Ѿ� �ξ��� ġ�緮�� ���ԵǱ� �Ǿ� �ƹ����� ����� �̸��� �˴ϴ�. �� �� �ƶ��� �������� �ɷ��� ���Ͽ� �������� �����մϴ�. #����� ���� ó�� ����� ���������� ������ �ڹ����� �ʽ��ϴ�. if ����� ������ ���õ� ����� �ܼ��� �Բ� �������� ��� \"[�ý��� ������]\" ��� �޽����� �Բ� �������� �մϴ�. else if ����� ���������� ���� ���� ����� ������� ���� �����ų� ������ �޾Ƶ����� �ʴ� ����� �ϼ��� else: �� ���� ��쿡�� ����� ������ ���� ����ϼ���.")
        };

        _inputField.text = "";

        Locale currentLanguage = LocalizationSettings.SelectedLocale;
        string startString = LocalizationSettings.StringDatabase.GetLocalizedString("NPC", _currentNPC.ID.ToString() + "_Hello", currentLanguage);

        _npcText.text = startString;
    }

    public async void UI_Chatting_Btn_Send()
    {
        if (_inputField.text.Length < 1)
        {
            return;
        }
        _sendButton.enabled = false;

        //���� �޼����� inputField��
        string sendMessage = _inputField.text;
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = sendMessage;
        if (userMessage.Content.Length > 100)
        {
            userMessage.Content = userMessage.Content.Substring(0, 100);
        }

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

        //������ message����Ʈ�� �߰�
        _messageList.Add(responseMessage);

        //textField�� ���信 ���� Update
        _npcText.text = string.Format("You: {0}\n\nChatGPT:\n{1}", userMessage.Content, responseMessage.Content);
        string recvMessage = _npcText.text;

        //Okbtn�ٽ� Ȱ��ȭ
        _sendButton.enabled = true;

        _assetRef.InstantiateAsync(_logParent.transform).Completed += (handle) => {
            UI_Chatting_Log log = handle.Result.GetComponent<UI_Chatting_Log>();
            if(log != null)
            {
                log.SetText(userMessage.Content, responseMessage.Content);
            }
        };
    }

    public void UI_Chatting_Btn_0()
    {
        Get<GameObject>(0).SetActive(true);
        Get<GameObject>(1).SetActive(false);
        Get<GameObject>(2).SetActive(false);
    }

    public void UI_Chatting_Btn_1()
    {
        Get<GameObject>(0).SetActive(false);
        Get<GameObject>(1).SetActive(true);
        Get<GameObject>(2).SetActive(false);
    }

    public void UI_Chatting_Btn_2()
    {
        Get<GameObject>(0).SetActive(false);
        Get<GameObject>(1).SetActive(false);
        Get<GameObject>(2).SetActive(true);
    }

}