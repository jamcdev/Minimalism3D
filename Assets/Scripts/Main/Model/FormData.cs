using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormData : MonoBehaviour
{
    public static string itemPhoto;
    public static string itemNickName;
    public static string itemPhoneNumber;
    public static string itemTimeForPickup;
    public static float itemX;
    public static float itemY;
    public static string itemStation;
    public static string itemTimePosted; // remove after 24 hours.

    public string itemPhotoJSON;
    public string itemNickNameJSON;
    public string itemPhoneNumberJSON;
    public string itemTimeForPickupJSON;
    public float itemXJSON;
    public float itemYJSON;
    public string itemStationJSON;
    public string itemTimePostedJSON;

    public string SaveToString()
    {
        // Manually save static to non-static for json to work. 

        itemPhotoJSON = itemPhoto;
        itemNickNameJSON = itemNickName;
        itemPhoneNumberJSON = itemPhoneNumber;
        itemTimeForPickupJSON = itemTimeForPickup;
        itemXJSON = itemX;
        itemYJSON = itemY;
        itemStationJSON = itemStation;
        itemTimePostedJSON = itemTimePosted;

        return JsonUtility.ToJson(this);
    }

    
}
