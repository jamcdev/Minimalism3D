using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class GetItemsFromDatabase : MonoBehaviour
{
    public GameObject photoFrame;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetWebData("https://minimal-server.herokuapp.com/items/", "HKU_Station"));
    }

    IEnumerator GetWebData(string address, string myID)
    {
        UnityWebRequest www = UnityWebRequest.Get(address + myID);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("WebRequest failed: " + www.error);
            yield break;
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            ProcessServerResponse(www.downloadHandler.text);
        }


    }

    void ProcessServerResponse(string rawResponse)
    {
        // Parse JSON 
        JSONNode node = JSON.Parse(rawResponse);

        // Output to console. 
        //Debug.Log(node[0]);
        Debug.Log(node.Count);
        Debug.Log(node[0]["_id"]);

        for(int i = 0; i < node.Count; i++)
        {
            Instantiate(photoFrame, new Vector3(node[i]["itemX"], 0.05f , node[i]["itemY"]), Quaternion.identity);
        }
    }
}
