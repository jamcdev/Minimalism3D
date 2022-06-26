using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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
        Scene scene = SceneManager.GetActiveScene();

        // FormData.itemPhoto saved in CameraTaker.cs
        FormData.itemNickName = nickName.text;
        FormData.itemPhoneNumber = phoneNumber.text;
        FormData.itemTimeForPickup = timeForPickup.text;
        //FormData.itemX = saved in PutPlaceholder.cs
        //FormData.itemY = saved in PutPlaceholder.cs
        FormData.itemStation = scene.name;
        FormData.itemTimePosted = System.DateTime.UtcNow.ToString();
        Debug.Log(FormData.itemTimePosted);


        //Debug.Log(FormData.itemPhoto);

        // Send Data to Server
        // Server saves to database

        StartCoroutine(sendFormDataToServer.PostWebData("https://minimal-server.herokuapp.com/test/", "user1"));
    }


}
