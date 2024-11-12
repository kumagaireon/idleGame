using UnityEngine;

namespace Kumagai.Entites
{
    public class SwipeData
    {
        public Vector2[] touchPositions = new Vector2[3];
        public bool isSwiping = false;
        public float lastTouchTime = 0.0f;
        public int swipeCount = 0;
        public bool isPressing = false;
    }
}