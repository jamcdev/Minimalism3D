using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormData : MonoBehaviour
{
    public static string itemPhoto;
    public static string itemNickName;
    public static string itemPhoneNumber;
    public static string itemTimeForPickup;

    public string itemPhotoJSON;
    public string itemNickNameJSON;
    public string itemPhoneNumberJSON;
    public string itemTimeForPickupJSON;

    public string SaveToString()
    {
        // Manually save static to non-static for json to work. 
        itemPhotoJSON = itemPhoto;
        itemNickNameJSON = itemNickName;
        itemPhoneNumberJSON = itemPhoneNumber;
        itemTimeForPickupJSON = itemTimeForPickup;

        return JsonUtility.ToJson(this);
        //return itemPhoto;
    }

    
}
