using Kumagai.Entites;
using UnityEngine;

namespace Kumagai.UseCase
{
    public interface ITapInputUseCase
    {
        void CheckTap(float longPressThreshold, GameObject cyalumeObj, SwipeData swipeData);
    }

    public class TapInputUseCase : ITapInputUseCase
    {
        private bool isPressing = false;
        private float pressStartTime;

        public void CheckTap(float longPressThreshold, GameObject cyalumeObj, SwipeData swipeData)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isPressing = true;
                pressStartTime = Time.time;
                cyalumeObj.SetActive(true);
            }
            else if (Input.GetMouseButtonUp(0) && isPressing)
            {
                isPressing = false;
                cyalumeObj.SetActive(false);
                float pressDuration = Time.time - pressStartTime;
                if (pressDuration >= longPressThreshold)
                {
                    //Debug.Log("Long Press Detected");
                }
            }
        }
    }
}