using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SwipeJudge2 : MonoBehaviour
{  
    // スワイプの方向が反転しているかを確認するメソッド
    // 入力された3点のベクトルが逆方向を向いているかどうかを判定
    public bool AreVectorsReversed(Vector2 a, Vector2 b, Vector2 c)
    {
        Vector2 vectorBA = a - b;
        Vector2 vectorBC = b - c;
        return Vector2.Dot(vectorBA, vectorBC) < 0;// ベクトルの内積が負の場合、反転しているとみなす
    }

    // スワイプの方向を検出してログに出力するメソッド
    // 入力された現在位置と前回位置を基にスワイプの方向を判定
    public void DetectSwipeDirection(Vector2 currentPosition, Vector2 previousPosition)
    {
        Vector2 direction = currentPosition - previousPosition;// 方向ベクトルを計算
        ScoreController.instance.GetPsylliumScore();
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))// 水平方向の移動が大きい場合
        {
         //   Debug.Log(direction.x > 0 ? "右スワイプ" : "左スワイプ"); // 右スワイプか左スワイプかを判定してログに出力
            //RhythmTest.Instance.AddInputTime(Time.time);// リズムテストインスタンスに入力時間を追加
        }
        else
        {
         //   Debug.Log(direction.y > 0 ? "上スワイプ" : "下スワイプ"); // 上スワイプか下スワイプかを判定してログに出力
            //RhythmTest.Instance.AddInputTime(Time.time);// リズムテストインスタンスに入力時間を追加
        }
    }
}
