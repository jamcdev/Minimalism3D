// Touch screen to send raycast and interact with collsion objects. s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchRaycast : MonoBehaviour
{
    public Text touchRaycastText;
    private string touchRaycastString = "No Data";
    private Touch touchObj;
    private Vector2 touchStartCoord, touchEndCoord;
    Ray touchRay;
    RaycastHit touchRayHit;

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
                    touchRaycastString = "Tapped.";
                    touchRay = Camera.main.ScreenPointToRay(touchStartCoord);
                    Debug.Log(touchRaycastString);

                    // Raycast hits collison detection.

                    if (Physics.Raycast(touchRay.origin, touchRay.direction, out touchRayHit, Mathf.Infinity))
                    {
                        Debug.Log(touchRayHit.collider.gameObject.name);
                        touchRaycastString = touchRayHit.collider.gameObject.name;

                        // Scene change to MTR name.

                        SceneManager.LoadScene(touchRaycastString);
                    }
                    else
                    {
                        Debug.Log("No hit");
                        touchRaycastString = "No hit.";
                    }

                }


                touchRaycastText.text = touchRaycastString;
            }
        }
    }
}

