// Put a placeholder on the MTR map.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPlaceholder : MonoBehaviour
{
    public GameObject RedBox;
    private Touch touchObj;
    private Vector2 touchStartCoord, touchEndCoord;
    private string touchRaycastString = "No Data";
    Ray touchRay;
    RaycastHit touchRayHit;
    float timeHeldDown = 0.0f;
    private int numPlaceholders = 0;
    public GameObject postManager;

    bool isPlaceNowToggled;

    public TokenManager tokenManager;

    public GameObject redPlaceholder;

    public GameObject NotifyCanvas;

    private void Start()
    {
        timeHeldDown = 0.0f;
        PlayerInfo.tokens = 1;
        isPlaceNowToggled = false;
    }

    public void togglePlace()
    {
        isPlaceNowToggled = true;
        NotifyCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1) // Touch to send raycast.
        {
            touchObj = Input.GetTouch(0);

            if (touchObj.phase == TouchPhase.Began)
            {
                touchStartCoord = touchObj.position;
                touchRaycastString = "Began.";
            }
            else if (touchObj.phase == TouchPhase.Moved || touchObj.phase == TouchPhase.Stationary)
            {
                touchEndCoord = touchObj.position;

                float dx = touchEndCoord.x - touchStartCoord.x;
                float dy = touchEndCoord.y - touchStartCoord.y;

                float threshold = 100;

                if (Mathf.Abs(dx) < threshold && Mathf.Abs(dy) < threshold)
                {
                    

                    touchRaycastString = "Tapped and Held down.";
                    touchRay = Camera.main.ScreenPointToRay(touchStartCoord);
                    Debug.Log(touchRaycastString);

                    // Raycast hits collison detection.

                    if (Physics.Raycast(touchRay.origin, touchRay.direction, out touchRayHit, Mathf.Infinity))
                    {
                        Debug.Log("PutPlaceholder: touchRayHit.point: " + touchRayHit.point);
                        touchRaycastString = touchRayHit.collider.gameObject.name;

                        timeHeldDown += Time.deltaTime;
                        //if(timeHeldDown > 3 && numPlaceholders < 1)
                        Debug.Log("PutPlaceholder: Player.tokens " + PlayerInfo.tokens);
                        Debug.Log("PutPlaceholder: isPlaceNowToggled " + isPlaceNowToggled);
                        if (PlayerInfo.tokens > 0 && isPlaceNowToggled == true)
                        {
                            NotifyCanvas.SetActive(false);

                            //numPlaceholders++;
                            isPlaceNowToggled = false;
                            PlayerInfo.tokens--;
                            tokenManager.updateTokenText();


                            // Box causes select item to pop up.
                            redPlaceholder = Instantiate(RedBox, touchRayHit.point, Quaternion.identity);
                            Handheld.Vibrate();

                            // Scene change to MTR name. (Change to item infomation, or add photo)
                            //SceneManager.LoadScene(touchRaycastString);
                            postManager.GetComponent<PostManager>().showCanvasPost();

                            FormData.itemX = touchRayHit.point.x;
                            FormData.itemY = touchRayHit.point.z;
                            Debug.Log("FormData.itemX " + FormData.itemX);
                            Debug.Log("FormData.itemY " + FormData.itemY);
                        }
                    }
                    else
                    {
                        Debug.Log("No hit");
                        touchRaycastString = "No hit.";
                    }

                }


                //touchRaycastText.text = touchRaycastString;
            }
        }
    }
}
