using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerTalker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Make a web request to get info from server. JSON/Text response. 
        StartCoroutine(GetWebData("http://localhost:8000/user/" , "myAwesomeID"));
        // Parse JSON 

        // Output to console. 
    }

    IEnumerator GetWebData(string address, string myID)
    {
        UnityWebRequest www = UnityWebRequest.Get(address + myID);

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
