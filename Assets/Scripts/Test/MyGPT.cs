using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class MyGPT : MonoBehaviour
{
    public TMP_InputField userInputField;
    public TMP_Text responseText;

    private readonly string openAIApiURL = "https://api.openai.com/v1/chat/completions";
    private readonly string apiKey = "sk-None-fuoYnG6WMkjR6sYESm9iT3BlbkFJfPECosg6CrpdR1s10NmK";

    public void OnSubmit()
    {
        string userInput = userInputField.text;
        if (!string.IsNullOrWhiteSpace(userInput))
        {
            StartCoroutine(SendRequestToChatGPT(userInput));
        }
    }

    private IEnumerator SendRequestToChatGPT(string userInput)
    {
        var request = new UnityWebRequest(openAIApiURL, "POST");
        string requestData = "{\"model\":\"gpt-3.5-turbo\",\"messages\":[{\"role\":\"system\",\"content\":\"You are a helpful assistant.\"},{\"role\":\"user\",\"content\":\"" + userInput + "\"}]}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            responseText.text = "Error: " + request.error;
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log("Response: " + response);
            ChatGPTResponse chatGPTResponse = JsonUtility.FromJson<ChatGPTResponse>(response);
            string gptResponse = chatGPTResponse.choices[0].message.content;
            responseText.text = gptResponse; //ChatGPT 응답을 UI에 표시
        }
    }
}

[System.Serializable]
public class ChatGPTResponse
{
    public Choice[] choices;
}

[System.Serializable]
public class Choice
{
    public Message message;
}
[System.Serializable]
public class Message
{
    public string content;
}