using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//使えない切り返しの判定ができてない

public class SwipeInputWithCount : MonoBehaviour
{
    public float followSpeed = 10f; // Cyalumeオブジェクトの追従速度
    public float swipeThreshold = 0.2f; // スワイプとして認識するための最小距離
    public float directionChangeThreshold = 0.5f; // スワイプ方向の変更を検出するための閾値
    [SerializeField] private Vector2[] touchPositions = new Vector2[3]; // タッチ位置を格納する配列
    private bool isSwiping = false; // スワイプ中かどうかを示すフラグ
    [SerializeField] private int swipeCount = 0; // スワイプの方向変更回数
    [SerializeField] GameObject Cyalume; // タップした時に出るオブジェクト
    private GameObject currentCyalume; // 現在生成されているCyalumeオブジェクト

    void Update()
    {
#if UNITY_EDITOR
        // PC の場合の入力処理
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(StartSwipe(Input.mousePosition));
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            StartCoroutine(UpdateSwipe(Input.mousePosition));
        }
        else if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            StartCoroutine(EndSwipe());
        }
#else
        // モバイルの入力処理
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                StartCoroutine(StartSwipe(touch.position));
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                StartCoroutine(UpdateSwipe(touch.position));
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                StartCoroutine(EndSwipe());
            }
        }
#endif

        // Cyalumeを現在のタッチ位置にスムーズに移動
        if (currentCyalume != null && isSwiping)
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPositions[0].x, touchPositions[0].y, 10));
            currentCyalume.transform.position = Vector3.Lerp(currentCyalume.transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    private IEnumerator StartSwipe(Vector2 position)
    {
        touchPositions[0] = touchPositions[1] = touchPositions[2] = position;
        isSwiping = true;

        // Cyalumeを生成して現在のタッチ位置に配置
        currentCyalume = Instantiate(Cyalume, Camera.main.ScreenToWorldPoint(new Vector3(touchPositions[0].x, touchPositions[0].y, 10)), Quaternion.identity);

        yield return null;
    }

    private IEnumerator UpdateSwipe(Vector2 position)
    {
        touchPositions[2] = touchPositions[1];
        touchPositions[1] = touchPositions[0];
        touchPositions[0] = position;

        // スワイプの方向が変わったかどうかをチェック
        if (Vector2.Distance(touchPositions[0], touchPositions[1]) > swipeThreshold && AreVectorsReversed(touchPositions[0], touchPositions[1], touchPositions[2]))
        {
            swipeCount++;
            Debug.Log("方向転換");
            DetectSwipeDirection(touchPositions[0], touchPositions[1]);
        }

        yield return null;
    }

    private IEnumerator EndSwipe()
    {
        isSwiping = false;
        Debug.Log("スワイプ総数: " + swipeCount);
        swipeCount = 0;

        // Cyalumeを削除
        if (currentCyalume != null)
        {
            Destroy(currentCyalume);
        }

        yield return null;
    }

    // スワイプの方向が逆かどうかを判定する関数
    private bool AreVectorsReversed(Vector2 a, Vector2 b, Vector2 c)
    {
        Vector2 vectorBA = a - b;
        Vector2 vectorBC = b - c;

        // スワイプ方向の変更を検出するための閾値を使用して判定
        return Vector2.Dot(vectorBA, vectorBC) < -directionChangeThreshold;
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