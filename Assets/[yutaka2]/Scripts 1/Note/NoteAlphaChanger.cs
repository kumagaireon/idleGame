using System.Threading.Tasks;
using UnityEngine;

public class NoteAlphaChanger : MonoBehaviour
{
    [Header("�ύX����")]
    [SerializeField] private float duration = 1.0f;
    [Header("�����A���t�@�l")]
    [SerializeField] private float firstAlpha = 0.3f;    

    private async Task OnFadeIn(SpriteRenderer targetSR)
    {
        //�󂯎����SpriteRenderer�̃A���t�@�l�̂ݕύX����
        Color color = targetSR.color;
        color.a = firstAlpha;
        targetSR.color = color;

        //���߂�FadeIn�ɕK�v�ȕϐ����Z�b�g����
        float startAlpha = color.a;     //Fade�̏����l
        float endAlpha = 1.0f;          //Fade�̍ŏI�l
        float elapsedTime = 0.0f;       //Fade�̒��Ԓl

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
