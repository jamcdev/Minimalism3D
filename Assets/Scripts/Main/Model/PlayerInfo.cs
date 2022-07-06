// This generates the unique device ID for reidentification with the database. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class PlayerInfo : MonoBehaviour
{
    static string DeviceID;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("DeviceID"))
        {
            DeviceID = PlayerPrefs.GetString("DeviceID");
            Debug.Log("Getting Existing: " + DeviceID);

            // Create new document in database.


        }
        else
        {
            DeviceID = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString("DeviceID", DeviceID);
            Debug.Log("Setting: " + DeviceID);

            // Grab player status (committed?)

        }

        StartCoroutine(PostWebData("https://minimal-server.herokuapp.com/userData/", "user1", DeviceID));
        //StartCoroutine(PostWebData("http://localhost:5000/userData/", "user1", DeviceID));
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }


    }

}
