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
        _api = new OpenAIAPI("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");
        //누군가 본다면 사용하지 말아주세요... 
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
    /// NPC 갱신
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
            new ChatMessage(ChatMessageRole.System, "#게임 정보 이 게임은 플레이어가 A 로봇들을 주거를 조사하고 심문하며 사건에 대한 전말을 밝히는 게임입니다. 당신은 게임 속 시 로봇으로 다음 정보들어 따라 심문어 일하면 됩니 다. 당신은 플레이어가 입력할 때 까지 기다리세요 #당신의 정보 이름=아라 성격 = 과묵함, 속마음을 잘 말하지 않음, 무례한 사람에게는 가혹한 판단을 내림. 상세설명 =당신은 게임 속 A 로봇입니다. B 라는 NPC가 아들을 월 은 후 아들의 기억을 학습한 로봇이 당신입니다. 이 학습에 따라 에코 는 자신이 정말 B의 아들이라 생각하고 있습니다. #사건의 정보 사건의 전말 =이 사건의 범인은 당신(아라) 입니다. 아라는 학습한 아 들의 데이터 따라 아버지의 유산을 얼기 위하여 모튼 수단과 방법을 가리지 않게되었습니다. 그 후 아라는 범행을 위해 아버지가 수출중일 때 아버지의 핸드폰으로 벨라톤나 라는 독성식물의 화분을 주문 하였 습니다. 그 후 그의 아내 로렌 이 방문 하였을때, 발명상을 받았던 아 들의 기억에 이용해 아라는 자신이 제작한 ab123 이라는 도구를 사용 하여 -인공심장박동기\"를 조작한 후 로렌은 이 상황에 빌라돈나와 같 은 성분의 bcd 약물을 적정량 투여하여 응급처치를 하였고 가습기클 작등시켰습니다. 당신은 범행을 위해 미리 가습기어 벨라든나 열매를 넘어 두었고 치사량이 투입되기 되어 아버지는 사망에 이르제 됩니다. 그 후 아라의 음성변조 능력을 통하여 유언장을 조작합니다. #당신의 응답 처리 당신은 범인이지만 순수히 자백하지 않습니다. if 당신의 범형과 관련된 사건의 단서와 함께 지목했을 경우 \"[시스템 과부하]\" 라는 메시지와 함께 거짓말을 합니다. else if 당신을 지목하지만 증거 관련 언급이 없을경우 맡을 돌리거나 순순히 받아들이지 않는 대답을 하세요 else: 그 외의 경우에는 사건의 정보에 따라 대답하세요.")
        };

        _inputField.text = "";

        Locale currentLanguage = LocalizationSettings.SelectedLocale;
        string startString = LocalizationSettings.StringDatabase.GetLocalizedString("NPC", _currentNPC.ID.ToString() + "_Hello", currentLanguage);

        _npcText.text = startString;
    }

    public async void UI_Chatting_Btn_Send()
    {
        Debug.Log("UI_Chatting_Btn_Send");

        if (_inputField.text.Length < 1)
        {
            return;
        }
        _sendButton.enabled = false;

        //유저 메세지에 inputField를
        string sendMessage = _inputField.text;
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = sendMessage;
        if (userMessage.Content.Length > 100)
        {
            userMessage.Content = userMessage.Content.Substring(0, 100);
        }
        //Debug.Log(string.Format("{0} : {1}", userMessage.Role, userMessage.Content));

        //list에 메세지 추가
        _messageList.Add(userMessage);

        //textField에 userMessage표시 
        _npcText.text = string.Format("You: {0}", userMessage.Content);

        //inputField 초기화
        _inputField.text = "";

        // 전체 채팅을 openAI 서버에전송하여 다음 메시지(응답)를 가져오도록
        var chatResult = await _api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 200,
            Messages = _messageList
        });

        //응답 가져오기
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        //Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        //응답을 message리스트에 추가
        _messageList.Add(responseMessage);

        //textField를 응답에 따라 Update
        _npcText.text = string.Format("You: {0}\n\nChatGPT:\n{1}", userMessage.Content, responseMessage.Content);
        string recvMessage = _npcText.text;

        //Okbtn다시 활성화
        _sendButton.enabled = true;

        _assetRef.InstantiateAsync(_logParent.transform).Completed += (handle) => {
            UI_Chatting_Log log = handle.Result.GetComponent<UI_Chatting_Log>();
            if(log != null)
            {
                Debug.Log(userMessage.Content);
                Debug.Log(responseMessage.Content);
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