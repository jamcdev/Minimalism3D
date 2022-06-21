using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureFormData : MonoBehaviour
{
    public Text nickName;
    public Text phoneNumber;
    public Text timeForPickup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Saves form data, send and close. 
    public void SaveFormData()
    {
        FormData.itemNickName = nickName.text;
        FormData.itemPhoneNumber = phoneNumber.text;
        FormData.itemTimeForPickup = timeForPickup.text;

        Debug.Log(FormData.itemPhoto);

        // Send Data to Server
        // Server saves to database


    }


}
