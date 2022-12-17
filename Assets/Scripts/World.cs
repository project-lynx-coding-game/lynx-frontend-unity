using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEngine.VFX;
using UnityEngine.UIElements;

[System.Serializable]
public class RuntimeResponse
{
    public string[] stdout;
    public string[] stderr;
}

public class CreateSessionRequest
{
    public string username;
}

public class SendCodeRequest
{
    public string username;
    public string[] code;
}

public class World : MonoBehaviour
{


    public string username = "testuser";
    public string sendCodeUrl = "https://server.blazej-smorawski.com";
    public List<GameObject> prefabs;
    public List<Object> objects;


    // -----============== Running Code ==============-----
    public Task createSessionTask;
    public Task runCodeTask;
    bool stopRunningCode = false;


    // -----============== UI ==============-----
    public TMPro.TMP_InputField inputField;
    public VisualEffect loadingEffect;
    [TextArea(6, 10)]
    public string defaultCode;


    void Start()
    {
        inputField.text = defaultCode;
    }

    public async void CreateSessionButtonPressed(UnityEngine.UI.Button button)
    {
        button.interactable = false;
        while (createSessionTask != null && !createSessionTask.IsCompleted)
        {
            await Task.Yield();
        }
        button.interactable = true;

        CreateSessionRequest request = new CreateSessionRequest();
        request.username = username;

        createSessionTask = RunRequest(JsonUtility.ToJson(request), "/create_session");
    }

    public async void RunButtonPressed(UnityEngine.UI.Button button)
    {
        if(runCodeTask != null && !runCodeTask.IsCompleted) 
        {
            button.interactable = false;
            stopRunningCode = true;
            while(stopRunningCode == true)
            {
                await Task.Yield();
            }
            button.interactable = true;
        }

        SendCodeRequest request = new SendCodeRequest();
        request.username = username;
        request.code = inputField.text.Split('\n');

        runCodeTask = RunRequest(JsonUtility.ToJson(request), "/send_code");
    }

    public async Task RunRequest(string request, string route)
    {
        loadingEffect.Play();
        string responseString = await SendQuery(request, route);
        loadingEffect.Stop();

        RuntimeResponse response = JsonUtility.FromJson<RuntimeResponse>(responseString);
        foreach (string log in response.stdout)
        {
            if (log.Contains("Action"))
            {
                Action action = Deserializer.GetAction(log);
                await action.Execute(this);
            }
            else if (log.Contains("Object"))
            {
                objects.Add(Deserializer.GetObject(log, prefabs));
            }

            if(stopRunningCode)
            {
                stopRunningCode = false;
                return;
            }
        }
    }

    private async Task<string> SendQuery(string query, string route)
    {
        var request = new UnityWebRequest(sendCodeUrl+route, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(query);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SendWebRequest();

        while (!request.isDone)
        {
            await Task.Yield();
        }

        Debug.Log("Status Code: " + request.responseCode + "\nBody:\n" + request.downloadHandler.text);
        return request.downloadHandler.text;
    }
}
