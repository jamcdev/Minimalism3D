// DPad control for camera panning and zooming.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualDPadCamera : MonoBehaviour
{
    public Text directionText;
    private Touch touchObj;
    private Vector2 touchStartCoord, touchEndCoord;
    private string directionString;
    public GameObject cameraTarget;
    public Camera mainCamera;
    public float scrollingSpeed = 0.005f;
    public float zoomingSpeed = 0.01f;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1) // Panning Camera
        {
            touchObj = Input.GetTouch(0);

            if (touchObj.phase == TouchPhase.Began)
            {
                touchStartCoord = touchObj.position;
            }
            else if (touchObj.phase == TouchPhase.Moved || touchObj.phase == TouchPhase.Stationary)
            {
                touchEndCoord = touchObj.position;

                float dx = touchEndCoord.x - touchStartCoord.x;
                float dy = touchEndCoord.y - touchStartCoord.y;

                float threshold = 100;

                if (Mathf.Abs(dx) < threshold && Mathf.Abs(dy) < threshold)
                {
                    directionString = "Tapped";
                }
                else if (dx > threshold && Mathf.Abs(dy) < threshold)
                {
                    directionString = "Right";
                    cameraTarget.transform.position += new Vector3(-scrollingSpeed, 0, scrollingSpeed);
                }
                else if (dx < -threshold && Mathf.Abs(dy) < threshold)
                {
                    directionString = "Left";
                    cameraTarget.transform.position += new Vector3(scrollingSpeed, 0, -scrollingSpeed);
                }
                else if (Mathf.Abs(dx) < threshold && dy > threshold)
                {
                    directionString = "Top";
                    cameraTarget.transform.position += new Vector3(0, scrollingSpeed * 2, 0);
                }
                else if (Mathf.Abs(dx) < threshold && dy < -threshold)
                {
                    directionString = "Bottom";
                    cameraTarget.transform.position += new Vector3(0, -scrollingSpeed * 2, 0);
                }
                else if (dx > threshold && dy > threshold)
                {
                    directionString = "Top-Right";
                    cameraTarget.transform.position += new Vector3(-scrollingSpeed * 2, 0, 0);
                }
                else if (dx > threshold && dy < -threshold)
                {
                    directionString = "Bottom-Right";
                    cameraTarget.transform.position += new Vector3(0, 0, scrollingSpeed * 2);
                }
                else if (dx < threshold && dy > threshold)
                {
                    directionString = "Top-Left";
                    cameraTarget.transform.position += new Vector3(0, 0, -scrollingSpeed * 2);
                }
                else if (dx < threshold && dy < -threshold)
                {
                    directionString = "Bottom-Left";
                    cameraTarget.transform.position += new Vector3(scrollingSpeed * 2, 0, 0);
                }


                directionText.text = directionString;
            }
        }
        else if (Input.touchCount == 2) // Zooming in camera. 
        {
            Touch touchObj_1 = Input.GetTouch(0);
            Touch touchObj_2 = Input.GetTouch(1);

            Vector2 touchStartCoord_1, touchEndCoord_1, touchStartCoord_2, touchEndCoord_2;

            float initialDisplacement = 0.0f;

            if (touchObj_1.phase == TouchPhase.Began || touchObj_2.phase == TouchPhase.Began)
            {
                touchStartCoord_1 = touchObj_1.position;
                touchStartCoord_2 = touchObj_2.position;

                initialDisplacement = Vector2.Distance(touchStartCoord_1, touchStartCoord_2);

                directionString = "Zoom: Began";
            }

            if ((touchObj_1.phase == TouchPhase.Moved || touchObj_1.phase == TouchPhase.Stationary) &&
                (touchObj_2.phase == TouchPhase.Moved || touchObj_2.phase == TouchPhase.Stationary))
            {
                touchEndCoord_1 = touchObj_1.position;
                touchEndCoord_2 = touchObj_2.position;

                float currentDisplacement = Vector2.Distance(touchEndCoord_1, touchEndCoord_2);

                if (currentDisplacement > initialDisplacement)
                {
                    mainCamera.orthographicSize += zoomingSpeed;
                    directionString = "Zoom: Zooming in.";
                }
                else if (currentDisplacement < initialDisplacement)
                {
                    mainCamera.orthographicSize -= zoomingSpeed;
                    directionString = "Zoom: Zooming Out.";
                }
            }

            directionText.text = directionString;
        }
    }
}
