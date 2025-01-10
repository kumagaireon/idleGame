using UnityEngine;

public class TapDetector : MonoBehaviour
{
    // 長押しと判定する時間の閾値
    public float longPressThreshold = 0.5f;
    // 現在押されているかどうかを示すフラグ
    private bool isPressing = false;
    // ボタンが押され始めた時間
    private float pressStartTime;

    // タップをチェックするメソッド
    public void CheckTap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressing = true;// 押されているフラグを立てる
            pressStartTime = Time.time;// 押された時刻を記録
        }
        else if (Input.GetMouseButtonUp(0) && isPressing)
        {
            isPressing = false;// 押されているフラグを下げる
            float pressDuration = Time.time - pressStartTime;// 押されていた時間を計算
            // 押されていた時間が閾値を超えている場合
            if (pressDuration >= longPressThreshold)
            {
                Debug.Log("長押しが検出されました"); // 長押しとしてログに出力
            }
        }
    }
}