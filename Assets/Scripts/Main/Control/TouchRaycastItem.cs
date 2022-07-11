// This runs when you touch the item you want more details about and claim. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class TouchRaycastItem : MonoBehaviour
{
    private Touch touchObj;
    private Vector2 touchStartCoord, touchEndCoord;
    Ray touchRay;
    RaycastHit touchRayHit;

    // Display selected image on canvas.
    public RawImage rawImage;

    public Text timePickupLabel;
    public Text PhoneNumberLabel;
    public GameObject avaliableItemCanvas;
    public GameObject infoCanvas;
    public GameObject claimButtonToHide;
    GameObject cube;

    // Canvas
    public GameObject canvasAvaliableItem;

    public TokenManager tokenManager;

    void Update()
    {
        if (Input.touchCount == 1) // Touch to send raycast.
        {
            touchObj = Input.GetTouch(0);

            if (touchObj.phase == TouchPhase.Began)
            {
                touchStartCoord = touchObj.position;
                //touchRaycastString = "Began.";
            }
            else if (touchObj.phase == TouchPhase.Moved || touchObj.phase == TouchPhase.Stationary)
            {
                touchEndCoord = touchObj.position;

                float dx = touchEndCoord.x - touchStartCoord.x;
                float dy = touchEndCoord.y - touchStartCoord.y;

                float threshold = 100;

                if (Mathf.Abs(dx) < threshold && Mathf.Abs(dy) < threshold)
                {
                    //touchRaycastString = "Tapped.";
                    touchRay = Camera.main.ScreenPointToRay(touchStartCoord);

                    // Raycast hits collison detection.

                    if (Physics.Raycast(touchRay.origin, touchRay.direction, out touchRayHit, Mathf.Infinity))
                    {
                        // Touch Item
                        Debug.Log("Item Touched: " + touchRayHit.collider.gameObject.name);

                        // Check that item is NOT "HKU_Station"

                        if (touchRayHit.collider.gameObject.name != "HKU_Station")
                        {

                            // Get item from array from database.
                            // OR get texture from collided object
                            Texture2D mat = (Texture2D)touchRayHit.collider.gameObject.GetComponent<Renderer>().material.mainTexture;

                            // Assign photo texture to rawImage of Canvas.

                            //rawImage.material = mat;
                            rawImage.texture = mat;
                            //rawImage.material.mainTexture = mat.mainTexture;

                            // Get Gameobject properties
                            cube = touchRayHit.collider.gameObject;
                            timePickupLabel.text = cube.GetComponent<ItemClass>().itemTimeForPickup;

                            // Show Canvas.
                            canvasAvaliableItem.SetActive(true);
                        }

                    }
                    else
                    {
                        Debug.Log("No hit.");
                    }

                }
            }
        }
    }

    public void ClaimItem()
    {
        if(PlayerInfo.tokens > 0)
        {
            PlayerInfo.tokens--;
            tokenManager.updateTokenText();

            PhoneNumberLabel.text = cube.GetComponent<ItemClass>().itemPhoneNumber;
            claimButtonToHide.SetActive(false);
            infoCanvas.SetActive(true);

            // update database. delete item.
            // post to server to request deletion.
            StartCoroutine(PostWebData("https://minimal-server.herokuapp.com/deleteItem/", "user1"));
        }
        else
        {
            Debug.Log("Not enough tokens!");
        }
    }

    // Delete item after claiming it.
    public IEnumerator PostWebData(string address, string myID)
    {
        Debug.Log("Sending delete item request.");

        // NB: double quoted needed inside brackets. Probably best to use a class. 

        // Convert all formdata to json;
        var srcStr = cube.GetComponent<ItemClass>().itemPhoneNumber;
        //var srcStr = deviceID + ',' + tokens.ToString();
        Debug.Log("Deleting: " + srcStr);

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

    public void ClosePanel()
    {
        avaliableItemCanvas.SetActive(false);
        claimButtonToHide.SetActive(true);
        infoCanvas.SetActive(false);
    }
}
