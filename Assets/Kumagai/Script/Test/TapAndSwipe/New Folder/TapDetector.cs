using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDetector : MonoBehaviour
{
    public float longPressThreshold = 0.5f; // 長押しとして認識するための時間
    private bool isPressing = false; // ボタンが押されているかどうかのフラグ
    private float pressStartTime; // ボタンが押され始めた時間

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
               // Debug.Log("長押し成功");
            }
            else
            {
              //  Debug.Log("単押し成功");
            }
        }
    }
}
