using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class SendFormDataToServer : MonoBehaviour
{
    public GameObject formDataObject;
    FormData formData;

    // Start is called before the first frame update
    void Start()
    {
        formData = formDataObject.GetComponent<FormData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Send png photo, nickname, phonenumber, time.
    public IEnumerator PostWebData(string address, string myID)
    {
        Debug.Log("Sending data.");

        // NB: double quoted needed inside brackets. Probably best to use a class. 

        // Convert formdata to json;
        var srcStr = formData.SaveToString();
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
