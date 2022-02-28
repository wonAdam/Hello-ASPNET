using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GameResult
{
    public string UserName;
    public int Score;
}

public class WebManager : MonoBehaviour
{
    string _baseUrl = "https://localhost:44338/api";
    GameResult _gameResult;
    // Start is called before the first frame update
    void Start()
    {
        GameResult res = new GameResult()
        { 
            Score = 999,
            UserName = "wondong"
        };

        StartCoroutine(CoSendWebRequest("ranking", "POST", res,
            (uwr) => 
            { 
                Debug.Log("Recv" + uwr.downloadHandler.text);
                _gameResult = JsonUtility.FromJson<GameResult>(uwr.downloadHandler.text);
            }
        ));

    }

    IEnumerator CoSendWebRequest(string url, string method, object obj, Action<UnityWebRequest> callback)
    {
        yield return null;

        string sendUrl = $"{_baseUrl}/{url}";

        byte[] jsonBytes = null;
        if(obj != null)
        {
            string jsonStr = JsonUtility.ToJson(obj);

            jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
        }

        var uwr = new UnityWebRequest(sendUrl, method);

        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            
            callback.Invoke(uwr);
        }
    }
}
