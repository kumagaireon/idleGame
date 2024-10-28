using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("タッチポジションを取得する間隔")]
    public float touchInterval = 0.1f;

    [Header("スワイプとして認識するための最小距離")]
    public float swipeThreshold = 2f;

    [SerializeField]
    private Vector2[] touchPositions = new Vector2[3];

    [Header("スワイプ中かどうかを示すフラグ")]
    private bool isSwiping = false;

    private TapDetector tapDetector;
    private SwipeJudge swipeJudge;

    private float lastTouchTime;

    private void Start()
    {
        tapDetector = GetComponent<TapDetector>();
        swipeJudge = GetComponent<SwipeJudge>();
    }

    private void Update()
    {
        tapDetector.CheckTap();
        SwipeInput();
    }

    private void SwipeInput()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            InitializeSwipe(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            UpdateSwipe(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndSwipe();
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                InitializeSwipe(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                UpdateSwipe(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                EndSwipe();
            }
        }
#endif
    }

    private void InitializeSwipe(Vector2 position)
    {
        touchPositions[0] = touchPositions[1] = touchPositions[2] = position;
        isSwiping = true;
        lastTouchTime = Time.time;
    }

    private void UpdateSwipe(Vector2 position)
    {
        if (Time.time - lastTouchTime >= touchInterval)
        {
            touchPositions[2] = touchPositions[1];
            touchPositions[1] = touchPositions[0];
            touchPositions[0] = position;
            lastTouchTime = Time.time;

            if (Vector2.Distance(touchPositions[0], touchPositions[1]) > swipeThreshold &&
                swipeJudge.AreVectorsReversed(touchPositions[0], touchPositions[1], touchPositions[2]))
            {
                swipeJudge.DetectSwipeDirection(touchPositions[0], touchPositions[1]);
            }
        }
    }

    private void EndSwipe()
    {
        isSwiping = false;
    }
}