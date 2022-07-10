// This runs when you touch the item you want more details about and claim. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            // update database.
        }
        else
        {
            Debug.Log("Not enough tokens!");
        }
    }

    public void ClosePanel()
    {
        avaliableItemCanvas.SetActive(false);
        claimButtonToHide.SetActive(true);
        infoCanvas.SetActive(false);
    }
}
