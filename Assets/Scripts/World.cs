using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;

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
    public List<Action> actions = new List<Action>();
    public List<Agent> agents;
    public InputField inputField;
    [TextArea(6, 10)]
    public string defaultCode;

    void Start()
    {
        inputField.text = defaultCode;
    }

    public async void RunCode()
    {
        RuntimeQuery query = new RuntimeQuery();
        query.code = inputField.text.Split('\n');

        string responseString = await SendQuery(JsonUtility.ToJson(query));

        RuntimeResponse response = JsonUtility.FromJson<RuntimeResponse>(responseString);
        foreach (string log in response.stdout)
        {
            if (log.Contains("Action"))
            {
                Action action = Deserializer.GetAction(log);
                actions.Add(action);
                await action.Execute(this);
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
