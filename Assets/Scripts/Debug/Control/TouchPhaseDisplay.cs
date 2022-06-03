// Get the status of user's touch on mobile. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchPhaseDisplay : MonoBehaviour
{
    public Text touchStatusText;
    private Touch touchObj;
    private float timeTouchEnded;
    private float displayTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            touchObj = Input.GetTouch(0);

            if(touchObj.phase == TouchPhase.Ended)
            {
                touchStatusText.text = touchObj.phase.ToString(); // "Ended".
                timeTouchEnded = Time.time;
            }
            else if ((Time.time - timeTouchEnded) > displayTime) 
            {
                touchStatusText.text = touchObj.phase.ToString(); // "Began" -> "Stationary."
                timeTouchEnded = Time.time;
            }
        }
        else if (Time.time - timeTouchEnded > displayTime)
        {
            touchStatusText.text = "Nothing touching.";
        }
    }
}
