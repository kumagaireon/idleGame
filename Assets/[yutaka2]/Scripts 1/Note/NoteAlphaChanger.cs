using System.Threading.Tasks;
using UnityEngine;

public class NoteAlphaChanger : MonoBehaviour
{
    [Header("変更時間")]
    [SerializeField] private float duration = 1.0f;
    [Header("初期アルファ値")]
    [SerializeField] private float firstAlpha = 0.3f;    

    private async Task OnFadeIn(SpriteRenderer targetSR)
    {
        //受け取ったSpriteRendererのアルファ値のみ変更する
        Color color = targetSR.color;
        color.a = firstAlpha;
        targetSR.color = color;

        //改めてFadeInに必要な変数をセットする
        float startAlpha = color.a;     //Fadeの初期値
        float endAlpha = 1.0f;          //Fadeの最終値
        float elapsedTime = 0.0f;       //Fadeの中間値

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            color.a = newAlpha;
            targetSR.color = color;
            await Task.Yield();
        }
    }

    public async void FadeIn(SpriteRenderer targetSR)
    {
        await OnFadeIn(targetSR);
    }
}
