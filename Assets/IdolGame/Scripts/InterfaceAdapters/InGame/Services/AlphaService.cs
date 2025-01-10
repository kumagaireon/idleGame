using Cysharp.Threading.Tasks;
using IdolGame.InGame.Interfaces;
using UnityEngine;

namespace IdolGame.InGame.Services;

public class AlphaService:IAlphaService
{
    [SerializeField] private float duration = 1.0f; // アルファ変化の所要時間
    [SerializeField] private float firstAlpha = 0.3f; // 初期アルファ値

    public async UniTask FadeIn(SpriteRenderer targetSr)
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
            await UniTask.Yield();
        }
    }
}