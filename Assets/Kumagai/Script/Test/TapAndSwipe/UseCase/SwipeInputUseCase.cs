using Kumagai.Entites;
using UnityEngine;

namespace Kumagai.UseCase
{
    public interface ISwipeinputUseCase
    {
        void IntitalizeSwipe(Vector2 position,SwipeData swipeData);
        void UpdateSwipe(Vector2 position, float touchInterval, float swipeThreshold, SwipeData swipeData);
        void EndSwipe(SwipeData swipeData);
    }
    public class SwipeInputUseCase:ISwipeinputUseCase
    {
        private readonly SwipeJudge swipeJudge;

        public SwipeInputUseCase(SwipeJudge swipeJudge)
        {
            this.swipeJudge = swipeJudge;
        }

        public void IntitalizeSwipe(Vector2 position, SwipeData swipeData)
        {
            swipeData.touchPositions[0] = swipeData.touchPositions[1] = swipeData.touchPositions[2] = position;
            swipeData.isSwiping = true;
        }

        public void UpdateSwipe(Vector2 position, float touchInterval, float swipeThreshold, SwipeData swipeData)
        {
            if (Time.time - swipeData.lastTouchTime >= touchInterval)
            {
                swipeData.touchPositions[2] = swipeData.touchPositions[1];
                swipeData.touchPositions[1] = swipeData.touchPositions[0];
                swipeData.touchPositions[0] = position;
                swipeData.lastTouchTime = Time.time;

                if (Vector2.Distance(swipeData.touchPositions[0], swipeData.touchPositions[1]) > swipeThreshold &&
                    swipeJudge.AreVectorsReversed(swipeData.touchPositions[0], swipeData.touchPositions[1],
                        swipeData.touchPositions[2]))
                {
                    swipeJudge.DetectSwipeDirection(swipeData.touchPositions[0], swipeData.touchPositions[1]);
                    swipeData.swipeCount++;
                }
            }
        }

        public void EndSwipe(SwipeData swipeData)
        {
            swipeData.isSwiping = false;
        }
    }

    public interface ITapInputUseCase
    {
        void CheckTap(float longPressThreshold);
    }

    public class TapInputUseCase : ITapInputUseCase
    {
        private bool isPressing = false;
        private float pressStartTime;

        public void CheckTap(float longPressThreshold)
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
                    Debug.Log("Long Press Detected");
                }
            }
        }
    }
}