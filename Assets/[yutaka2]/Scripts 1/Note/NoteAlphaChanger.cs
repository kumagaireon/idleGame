using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NoteAlphaChanger : MonoBehaviour
{
    [Header("変化時間")] [SerializeField] private float duration = 1.0f; // アルファ変化の所要時間
    [Header("初期アルファ値")] [SerializeField] private float firstAlpha = 0.3f; // 初期アルファ値

    /// <summary>
    /// SpriteRendererのアルファ値をフェードインさせるメソッド
    /// </summary>
    /// <param name="targetSr">対象のSpriteRenderer</param>
    private async UniTask OnFadeIn(SpriteRenderer targetSr)
    {
        // 渡されたSpriteRendererのアルファ値を初期値に設定
        Color color = targetSr.color;
        color.a = firstAlpha;
        targetSr.color = color;

        // フェードインに必要な変数を設定
        float startAlpha = color.a; // フェードの開始アルファ値
        float endAlpha = 1.0f; // フェードの終了アルファ値
        float elapsedTime = 0.0f; // 経過時間

        // フェードインの処理
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            color.a = newAlpha;
            targetSr.color = color;
            await Task.Yield();
        }
    }

    /// <summary>
    /// フェードインを開始する公開メソッド
    /// </summary>
    /// <param name="targetSr">対象のSpriteRenderer</param>
    public async void FadeIn(SpriteRenderer targetSr)
    {
        await OnFadeIn(targetSr);
    }
}
