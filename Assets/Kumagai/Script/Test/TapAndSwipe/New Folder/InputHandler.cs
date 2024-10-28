using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("タッチポジションを取得する間隔")]
    public float touchInterval = 0.1f; // タッチポジションを取得する間隔

    [Header("スワイプとして認識するための最小距離")]
    public float swipeThreshold = 2f; // スワイプとして認識するための最小距離

    [SerializeField]
    [Header("タッチ位置を格納する配列")]
    private Vector2[] touchPositions = new Vector2[3]; // タッチ位置を格納する配列

    [Header("スワイプ中かどうかを示すフラグ")]
    private bool isSwiping = false; // スワイプ中かどうかを示すフラグ

    public bool isSwip = false;
    [SerializeField]
    [Header("スワイプの方向変更回数")]
    private int swipeCount = 0; // スワイプの方向変更回数

    [Header("最後にタッチ位置を取得した時間")]
    private float lastTouchTime; // 最後にタッチ位置を取得した時間

    private TapDetector tapDetector; // 単押しと長押しの検出器
    private SwipeJudge swipeJudge; // スワイプ判定のクラス

    // 単押し回数をカウントするプロパティ
    public int TapCount { get; private set; } = 0;
    public int LengthCount { get; private set; } = 0;

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

    public void IncrementTapCount()
    {
        if (isSwip)
        {
            TapCount++;
            Debug.Log("単押し回数: " + TapCount);
        }
    }

    public void IncrementLengthCount()
    {
        if (isSwip)
        {
            LengthCount++;
            Debug.Log("単押し回数: " + TapCount);
        }
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
            //  EndSwipe();
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
             //   EndSwipe();
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

            if (Vector2.Distance(touchPositions[0], touchPositions[1]) > swipeThreshold && swipeJudge.AreVectorsReversed(touchPositions[0], touchPositions[1], touchPositions[2]))
            {
                swipeCount++;
                isSwip = true;
              //  Debug.Log("方向転換");
                swipeJudge.DetectSwipeDirection(touchPositions[0], touchPositions[1]);
            }
            else
            {
                isSwip = false;
            }
        }
    }

    private void EndSwipe()
    {
        if (isSwiping)
        {
            Debug.Log("スワイプ総数: " + swipeCount);
            swipeCount = 0;
            isSwiping = false;
        }
    }
}