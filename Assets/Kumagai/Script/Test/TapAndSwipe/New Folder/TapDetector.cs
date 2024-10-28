using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDetector : MonoBehaviour
{
    public float longPressThreshold = 0.5f;
    private bool isPressing = false;
    private float pressStartTime;

    public void CheckTap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressing = true;
            pressStartTime = Time.time;
        }
        else if (Input.GetMouseButtonUp(0) && isPressing)
        {
            isPressing = false;
            float pressDuration = Time.time - pressStartTime;

            if (pressDuration >= longPressThreshold)
            {
                Debug.Log("’·‰Ÿ‚µ¬Œ÷");
            }
        }
    }
}