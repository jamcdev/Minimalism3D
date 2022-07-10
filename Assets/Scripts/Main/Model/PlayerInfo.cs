// This generates the unique device ID for reidentification with the database. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class PlayerInfo : MonoBehaviour
{
    static public string DeviceID;
    static public int tokens;

    // non-static json
    public string DeviceIDJSON;
    public int tokensJSON;

    public TokenManager tokenManager;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("DeviceID")) // Player exist?
        {
            DeviceID = PlayerPrefs.GetString("DeviceID");
            Debug.Log("Getting Existing: " + DeviceID);

            // Grab player status (committed?)
            StartCoroutine(GetWebData("https://minimal-server.herokuapp.com/userData/", DeviceID));


        }
        else
        {
            DeviceID = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString("DeviceID", DeviceID);
            Debug.Log("Setting: " + DeviceID);

            
            // Create new document in database.
            StartCoroutine(PostWebData("https://minimal-server.herokuapp.com/userData/", "user1", DeviceID));
            //StartCoroutine(PostWebData("http://localhost:5000/userData/", "user1", DeviceID));

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string SaveToString()
    {
        // Manually save static to non-static for json to work. 

        DeviceIDJSON = DeviceID;
        tokensJSON = tokens;

        return JsonUtility.ToJson(this);
    }

    // Send png photo, nickname, phonenumber, time.
    public IEnumerator PostWebData(string address, string myID, string deviceID)
    {
        Debug.Log("Sending data.");

        // NB: double quoted needed inside brackets. Probably best to use a class. 

        // Convert all formdata to json;
        //var srcStr = formData.SaveToString();
        var srcStr = deviceID;
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
            tokenManager.updateTokenText();
        }
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

        Debug.Log("PlayerData raw: " + rawResponse);
        // Parse JSON 
        JSONNode node = JSON.Parse(rawResponse);

        // Save to static variable
        //ItemsFromDatabase.avaliableItems = node;

        // Output to console. 
        //Debug.Log(node[0]);
        //Debug.Log(node.Count);
        //Debug.Log(node[0]["_id"]);
        Debug.Log("Player Data: " + node[0]);

        tokens = node[0]["tokens"];
        Debug.Log("tokens update: " + tokens);

        tokenManager.updateTokenText();

    }

}
