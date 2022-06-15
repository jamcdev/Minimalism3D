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
                timeHeldDown = 0.0f;
            }
            else if (touchObj.phase == TouchPhase.Moved || touchObj.phase == TouchPhase.Stationary)
            {
                touchEndCoord = touchObj.position;
                timeHeldDown += Time.deltaTime;

                float dx = touchEndCoord.x - touchStartCoord.x;
                float dy = touchEndCoord.y - touchStartCoord.y;

                float threshold = 100;

                if (Mathf.Abs(dx) < threshold && Mathf.Abs(dy) < threshold && timeHeldDown > 2)
                {
                    timeHeldDown = 0.0f;

                    touchRaycastString = "Tapped and Held down.";
                    touchRay = Camera.main.ScreenPointToRay(touchStartCoord);
                    Debug.Log(touchRaycastString);

                    // Raycast hits collison detection.

                    if (Physics.Raycast(touchRay.origin, touchRay.direction, out touchRayHit, Mathf.Infinity))
                    {
                        Debug.Log(touchRayHit.point);
                        touchRaycastString = touchRayHit.collider.gameObject.name;

                        Instantiate(RedBox, touchRayHit.point, Quaternion.identity);
                        Handheld.Vibrate();
                        // Scene change to MTR name. (Change to item infomation, or add photo)
                        //SceneManager.LoadScene(touchRaycastString);
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
