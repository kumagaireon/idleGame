using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputWithCount : MonoBehaviour
{
    public float touchInterval = 0.1f; // タッチポジションを取得する間隔
    public float swipeThreshold = 2f; // スワイプとして認識するための最小距離
    [SerializeField] private Vector2[] touchPositions = new Vector2[3]; // タッチ位置を格納する配列
    private bool isSwiping = false; // スワイプ中かどうかを示すフラグ
    [SerializeField] private int swipeCount = 0; // スワイプの方向変更回数
    private float lastTouchTime; // 最後にタッチ位置を取得した時間

    void Update()
    {
#if UNITY_EDITOR
        // PC の場合の入力処理
        if (Input.GetMouseButtonDown(0))
        {
            // マウスボタンが押されたとき、タッチ位置を初期化
            touchPositions[0] = touchPositions[1] = touchPositions[2] = Input.mousePosition;
            isSwiping = true;
            lastTouchTime = Time.time;
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            // マウスボタンが押されている間、定期的にタッチ位置を更新
            if (Time.time - lastTouchTime >= touchInterval)
            {
                touchPositions[2] = touchPositions[1];
                touchPositions[1] = touchPositions[0];
                touchPositions[0] = Input.mousePosition;
                lastTouchTime = Time.time;

                // スワイプの方向が変わったかどうかをチェック
                if (Vector2.Distance(touchPositions[0], touchPositions[1]) > swipeThreshold && AreVectorsReversed(touchPositions[0], touchPositions[1], touchPositions[2]))
                {
                    swipeCount++;
                    Debug.Log("方向転換");
                    DetectSwipeDirection(touchPositions[0], touchPositions[1]);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            // マウスボタンが離されたとき、スワイプの総数をログに出力
            isSwiping = false;
            Debug.Log("スワイプ総数: " + swipeCount);
            swipeCount = 0;
        }
#else
        // モバイルの入力処理
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // タッチが始まったとき、タッチ位置を初期化
                touchPositions[0] = touchPositions[1] = touchPositions[2] = touch.position;
                isSwiping = true;
                lastTouchTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                // タッチが移動している間、定期的にタッチ位置を更新
                if (Time.time - lastTouchTime >= touchInterval)
                {
                    touchPositions[2] = touchPositions[1];
                    touchPositions[1] = touchPositions[0];
                    touchPositions[0] = touch.position;
                    lastTouchTime = Time.time;

                    // スワイプの方向が変わったかどうかをチェック
                    if (Vector2.Distance(touchPositions[0], touchPositions[1]) > swipeThreshold && AreVectorsReversed(touchPositions[0], touchPositions[1], touchPositions[2]))
                    {
                        swipeCount++;
                        Debug.Log("方向転換");
                        DetectSwipeDirection(touchPositions[0], touchPositions[1]);
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                // タッチが終了したとき、スワイプの総数をログに出力
                isSwiping = false;
                Debug.Log("スワイプ総数: " + swipeCount);
                swipeCount = 0;
            }
        }
#endif
    }

    // スワイプの方向が逆かどうかを判定する関数
    private bool AreVectorsReversed(Vector2 a, Vector2 b, Vector2 c)
    {
        Vector2 vectorBA = a - b;
        Vector2 vectorBC = b - c;
        return Vector2.Dot(vectorBA, vectorBC) < 0;
    }

    // スワイプの方向を検出してログに出力する関数
    private void DetectSwipeDirection(Vector2 currentPosition, Vector2 previousPosition)
    {
        Vector2 direction = currentPosition - previousPosition;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            Debug.Log(direction.x > 0 ? "右スワイプ" : "左スワイプ");
        }
        else
        {
            Debug.Log(direction.y > 0 ? "上スワイプ" : "下スワイプ");
        }
    }
}
