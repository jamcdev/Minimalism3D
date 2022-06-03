using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualDPad : MonoBehaviour
{
    public Text directionText;
    private Touch touchObj;
    private Vector2 touchStartCoord, touchEndCoord;
    private string directionString;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touchObj = Input.GetTouch(0);

            if (touchObj.phase == TouchPhase.Began)
            {
                touchStartCoord = touchObj.position;
            }
            else if (touchObj.phase == TouchPhase.Moved || touchObj.phase == TouchPhase.Ended)
            {
                touchEndCoord = touchObj.position;

                float dx = touchEndCoord.x - touchStartCoord.x;
                float dy = touchEndCoord.y - touchStartCoord.y;

                float threshold = 100;

                if (Mathf.Abs(dx) < threshold && Mathf.Abs(dy) < threshold)
                    directionString = "Tapped";

                else if (dx > threshold && Mathf.Abs(dy) < threshold)
                    directionString = "Right";
                else if (dx < -threshold && Mathf.Abs(dy) < threshold)
                    directionString = "Left";
                else if (Mathf.Abs(dx) < threshold && dy > threshold)
                    directionString = "Top";
                else if (Mathf.Abs(dx) < threshold && dy < -threshold)
                    directionString = "Bottom";

                else if (dx > threshold && dy > threshold)
                    directionString = "Top-Right";
                else if (dx > threshold && dy < -threshold)
                    directionString = "Bottom-Right";
                else if (dx < threshold && dy > threshold)
                    directionString = "Top-Left";
                else if (dx < threshold && dy < -threshold)
                    directionString = "Bottom-Left";


                directionText.text = directionString;
            }
        }
    }
}
