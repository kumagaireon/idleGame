using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NoteShaker : MonoBehaviour
{
    private List<int> shakedCount = new List<int>();

    [Header("�^�b�v�\�I�u�W�F�N�g")]
    [SerializeField] private GameObject shakeAbleObject;

    //�^�b�v�\����
    public async Task OnShakeAble()
    {
        //���͂̎�t�J�n
        //InputChecker.instance.SetShakeAble();
        shakeAbleObject.SetActive(true);

        //=====���͂̉񐔃J�E���g=====
        int counter = 0;
        float timer = 0;
        while (timer < 1.0f)
        {
            timer += Time.deltaTime;
            if (InputChecker.instance.InputShake())
            {
                counter++;
                ScoreController.instance.GetPsylliumScore();
            }
            await Task.Yield();
        }
        //============================

        //���͂̎�t�I��
        Debug.Log("shake�I�� count:" + counter);
        shakedCount.Add(counter);
        shakeAbleObject.SetActive(false);
        //InputChecker.instance.SetShakeNotAble();
    }
}
