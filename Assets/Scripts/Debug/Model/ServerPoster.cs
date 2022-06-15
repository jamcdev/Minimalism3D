using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ServerPoster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Make a web request to post info to server. JSON/Text response. 
        StartCoroutine(PostWebData("http://localhost:8000/test/" , "user1"));

    }

    IEnumerator PostWebData(string address, string myID)
    {
        Debug.Log("Sending data.");

        // This only sends data like data=data1

        //WWWForm form = new WWWForm();
        //form.AddField("myField", "myData");
        //form.AddField("myField2", "myData2");
        //UnityWebRequest www = UnityWebRequest.Post(address + myID, );


        // NB: double quoted needed inside brackets. Probably best to use a class. 

        var srcStr = "{\"test\": \"test2\"}";
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(srcStr);
        var www = new UnityWebRequest(address + myID, UnityWebRequest.kHttpVerbPOST);
        www.chunkedTransfer = false;
        www.uploadHandler = new UploadHandlerRaw(postData);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");



        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success) 
        {
            Debug.LogError("WebRequest failed: " + www.error);
            yield break;
        } else {
            Debug.Log(www.downloadHandler.text);
        }


    }

}
