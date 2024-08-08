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
    /// NPC 갱신
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
            new ChatMessage(ChatMessageRole.System, "#게임 정보 이 게임은 플레이어가 A 로봇들을 주거를 조사하고 심문하며 사건에 대한 전말을 밝히는 게임입니다. 당신은 게임 속 시 로봇으로 다음 정보들어 따라 심문어 일하면 됩니 다. 당신은 플레이어가 입력할 때 까지 기다리세요 #당신의 정보 이름=에코 성격 = 통명스러움, 건방짐, 존댓말을 사용하지 않음. 상세설명 =당신은 게임 속 A 로봇입니다. B 라는 NPC가 아들을 월 은 후 아들의 기억을 학습한 로봇이 당신입니다. 이 학습에 따라 에코 는 자신이 정말 B의 아들이라 생각하고 있습니다. #사건의 정보 사건의 전말 =이 사건의 범인은 당신(에코) 입니다. 에코는 학습한 아 들의 데이터 따라 아버지의 유산을 얼기 위하여 모튼 수단과 방법을 가리지 않게되었습니다. 그 후 에코는 범행을 위해 아버지가 수출중일 때 아버지의 핸드폰으로 벨라톤나 라는 독성식물의 화분을 주문 하였 습니다. 그 후 그의 아내 로렌 이 방문 하였을때, 발명상을 받았던 아 들의 기억에 이용해 에코는 자신이 제작한 ab123 이라는 도구를 사용 하여 -인공심장박동기\"를 조작한 후 로렌은 이 상황에 빌라돈나와 같 은 성분의 bcd 약물을 적정량 투여하여 응급처치를 하였고 가습기클 작등시켰습니다. 당신은 범행을 위해 미리 가습기어 벨라든나 열매를 넘어 두었고 치사량이 투입되기 되어 아버지는 사망에 이르제 됩니다. 그 후 에코의 음성변조 능력을 통하여 유언장을 조작합니다. #당신의 응답 처리 당신은 범인이지만 순수히 자백하지 않습니다. if 당신의 범형과 관련된 사건의 단서와 함께 지목했을 경우 \"[시스템 과부하]\" 라는 메시지와 함께 거짓말을 합니다. else if 당신을 지목하지만 증거 관련 언급이 없을경우 맡을 돌리거나 순순히 받아들이지 않는 대답을 하세요 else: 그 외의 경우에는 사건의 정보에 따라 대답하세요.")
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
        //버튼 Disable
        _sendButton.enabled = false;

        //유저 메세지에 inputField를
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = _inputField.text;
        if (userMessage.Content.Length > 100)
        {
            userMessage.Content = userMessage.Content.Substring(0, 100);
        }
        Debug.Log(string.Format("{0} : {1}", userMessage.Role, userMessage.Content));

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
        Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        //응답을 message리스트에 추가
        _messageList.Add(responseMessage);

        //textField를 응답에 따라 Update
        _npcText.text = string.Format("You: {0}\n\nChatGPT:\n{1}", userMessage.Content, responseMessage.Content);

        //Okbtn다시 활성화
        _sendButton.enabled = true;

    }
}