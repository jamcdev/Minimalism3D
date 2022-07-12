using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;

public class ButtonManager : MonoBehaviour
{
    public PlayerInfo playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scene_Main()
    {
        // Update user's token numbers when leaving mtr.

        // Post update request with new (deviceID, token numbers)

        StartCoroutine(PostWebData("https://minimal-server.herokuapp.com/updateTokens/", "user1"));

        SceneManager.LoadScene("MAIN_MTR");
    }

    // Send deviceID + new token count for updating the database.
    public IEnumerator PostWebData(string address, string myID)
    {
        Debug.Log("Sending latest token data.");

        // NB: double quoted needed inside brackets. Probably best to use a class. 

        // Convert all formdata to json;
        var srcStr = playerInfo.SaveToString();
        //var srcStr = deviceID + ',' + tokens.ToString();
        Debug.Log(srcStr);

        //var srcStr = "{\"test\": \"test2\"}";
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(srcStr);
        var www = new UnityWebRequest(address + myID, UnityWebRequest.kHttpVerbPOST);
        www.chunkedTransfer = false;
        www.uploadHandler = new UploadHandlerRaw(postData);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");



        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("WebRequest failed: " + www.error);
            yield break;
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}

