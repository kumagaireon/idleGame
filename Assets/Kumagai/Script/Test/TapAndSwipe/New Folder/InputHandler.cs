using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("タッチポジションを取得する間隔")]
    public float touchInterval = 0.1f;

    [Header("スワイプと認識するための最小距離")]
    public float swipeThreshold = 2f;

    private Vector2[] touchPositions = new Vector2[3];

    [Header("スワイプが現在行われているかどうかのフラグ")]
    private bool isSwiping = false;

    private TapDetector tapDetector;// タップを検出するためのコンポーネント
    private SwipeJudge swipeJudge;// スワイプを判定するためのコンポーネント

    private float lastTouchTime;// 最後にタッチを検出した時間

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
            InitializeSwipe(Input.mousePosition);// スワイプの初期化
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            UpdateSwipe(Input.mousePosition);// スワイプの更新
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndSwipe();// スワイプの終了
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                InitializeSwipe(touch.position);// スワイプの初期化
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                UpdateSwipe(touch.position);// スワイプの更新
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                EndSwipe();// スワイプの終了
            }
        }
#endif
    }

    // スワイプの初期化を行うメソッド
    private void InitializeSwipe(Vector2 position)
    {
        touchPositions[0] = touchPositions[1] = touchPositions[2] = position;// タッチポジションを初期化
        isSwiping = true;// スワイプ中フラグを立てる
        lastTouchTime = Time.time;// 最後のタッチ時間を記録
    }
    
    // スワイプの更新を行うメソッド
    private void UpdateSwipe(Vector2 position)
    {
        if (Time.time - lastTouchTime >= touchInterval)// タッチ間隔を満たした場合
        {
            touchPositions[2] = touchPositions[1];
            touchPositions[1] = touchPositions[0];
            touchPositions[0] = position;
            lastTouchTime = Time.time;
            
            // スワイプが閾値を超え、ベクトルが逆転している場合
            if (Vector2.Distance(touchPositions[0], touchPositions[1]) > swipeThreshold &&
                swipeJudge.AreVectorsReversed(touchPositions[0], touchPositions[1], touchPositions[2]))
            {
                // スワイプ方向を判定
                swipeJudge.DetectSwipeDirection(touchPositions[0], touchPositions[1]);
            }
        }
    }
    
// スワイプの終了を行うメソッド
    private void EndSwipe()
    {
        isSwiping = false;
    }
}