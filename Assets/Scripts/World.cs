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

public class RuntimeQuery
{
    public string[] code;
}

public class World : MonoBehaviour
{
    public string sendCodeUrl = "https://server.blazej-smorawski.com/send_code";
    public List<GameObject> prefabs;
    public List<Object> objects;

    // -----============== Running Code ==============-----
    public Task runCodeTask;
    bool stop = false;
    bool stopped = false;

    // -----============== UI ==============-----
    public TMPro.TMP_InputField inputField;
    public VisualEffect loadingEffect;
    [TextArea(6, 10)]
    public string defaultCode;

    void Start()
    {
        inputField.text = defaultCode;
    }

    public async void RunButtonPressed(UnityEngine.UI.Button button)
    {
        if(runCodeTask != null && runCodeTask.Status != TaskStatus.RanToCompletion) 
        {
            button.interactable = false;
            stop = true;
            while(stop == true)
            {
                await Task.Yield();
            }
            button.interactable = true;
        }

        runCodeTask = RunCode();
    }

    public async Task RunCode()
    {
        RuntimeQuery query = new RuntimeQuery();
        query.code = inputField.text.Split('\n');

        loadingEffect.Play();
        string responseString = await SendQuery(JsonUtility.ToJson(query));
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

            if(stop)
            {
                stop = false;
                return;
            }
        }
    }

    private async Task<string> SendQuery(string query)
    {
        var request = new UnityWebRequest(sendCodeUrl, "POST");
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
