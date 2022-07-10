using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;

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

        // Save to static variable
        ItemsFromDatabase.avaliableItems = node;

        // Output to console. 
        //Debug.Log(node[0]);
        Debug.Log(node.Count);
        Debug.Log(node[0]["_id"]);

        for(int i = 0; i < node.Count; i++)
        {
            //Instantiate(photoFrame, new Vector3(node[i]["itemX"], 0.05f , node[i]["itemY"]), Quaternion.identity);

            // Convert images 
            byte[] imageBytes = Convert.FromBase64String(node[i]["itemPhoto"]);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imageBytes);

            //Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            GameObject cube = Instantiate(photoFrame, new Vector3(node[i]["itemX"], 0.05f, node[i]["itemY"]), Quaternion.identity);
            cube.name = node[i]["_id"];
            Material mat = cube.GetComponent<Renderer>().material;
            mat.mainTexture = tex;

            cube.GetComponent<ItemClass>().itemPhoto = node[i]["itemPhoto"];
            cube.GetComponent<ItemClass>().itemNickName = node[i]["itemNickName"];
            cube.GetComponent<ItemClass>().itemPhoneNumber = node[i]["itemPhoneNumber"];
            cube.GetComponent<ItemClass>().itemTimeForPickup = node[i]["itemTimeForPickup"];
            cube.GetComponent<ItemClass>().itemStation = node[i]["itemStation"];
        }
    }
}
