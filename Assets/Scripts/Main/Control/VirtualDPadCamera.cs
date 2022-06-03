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
    public float scrollingSpeed = 0.005f;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            touchObj = Input.GetTouch(0);

            if (touchObj.phase == TouchPhase.Began)
            {
                touchStartCoord = touchObj.position;
            }
            //else if (touchObj.phase == TouchPhase.Moved || touchObj.phase == TouchPhase.Ended)
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
    }
}
