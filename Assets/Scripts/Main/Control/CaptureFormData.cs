using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureFormData : MonoBehaviour
{
    public Text nickName;
    public Text phoneNumber;
    public Text timeForPickup;

    public GameObject sendFormDataToServerObject;
    SendFormDataToServer sendFormDataToServer;

    // Start is called before the first frame update
    void Start()
    {
        sendFormDataToServer = sendFormDataToServerObject.GetComponent<SendFormDataToServer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Saves form data, send and close. 
    public void SaveFormData()
    {
        // FormData.itemPhoto saved in CameraTaker.cs
        FormData.itemNickName = nickName.text;
        FormData.itemPhoneNumber = phoneNumber.text;
        FormData.itemTimeForPickup = timeForPickup.text;

        //Debug.Log(FormData.itemPhoto);

        // Send Data to Server
        // Server saves to database

        StartCoroutine(sendFormDataToServer.PostWebData("http://localhost:8000/test/", "user1"));
    }


}
