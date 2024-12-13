using Kumagai.Entites;
using Kumagai.InterfaceAdapters;
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
           // swipeData.isSwiping = true;
        }

        // ReSharper disable Unity.PerformanceAnalysis
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
          //  swipeData.isSwiping = false;
        }
    }
}