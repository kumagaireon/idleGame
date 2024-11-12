using UnityEngine;

namespace Kumagai.InterfaceAdapters
{
    public class SwipeJudge : MonoBehaviour
    {
        public bool AreVectorsReversed(Vector2 a, Vector2 b, Vector2 c)
        {
            Vector2 vectorBA = a - b;
            Vector2 vectorBC = b - c;
            return Vector2.Dot(vectorBA, vectorBC) < 0;
        }

        public void DetectSwipeDirection(Vector2 currentPosition, Vector2 previousPosition)
        {
            Vector2 direction = currentPosition - previousPosition;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                //  Debug.Log(direction.x > 0 ? "Right Swipe" : "Left Swipe");
                RhythmTest.Instance.AddInputTime(Time.time);
            }
            else
            {
                //   Debug.Log(direction.y > 0 ? "Up Swipe" : "Down Swipe");
                RhythmTest.Instance.AddInputTime(Time.time);
            }
        }
    }
}