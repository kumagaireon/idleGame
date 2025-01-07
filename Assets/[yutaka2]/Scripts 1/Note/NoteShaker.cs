using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NoteShaker : MonoBehaviour
{
    private List<int> shakedCount = new List<int>();

    [Header("�^�b�v�\�I�u�W�F�N�g")]
    [SerializeField] private GameObject shakeAbleObject;

    public static bool shakeAble = false;

    //�^�b�v�\����
    public async Task OnShakeAble()
    {
        
        //���͂̎�t�J�n
        //InputChecker.instance.SetShakeAble();
        shakeAbleObject.SetActive(true);
        shakeAble = true;

        //=====���͂̉񐔃J�E���g=====
        int counter = 0;
        float timer = 0;
        while (timer < 1.0f)
        {
            timer += Time.deltaTime;
            //if (InputChecker.instance.InputShake())
            //{
            //    counter++;
            //    Debug.Log(counter);
            //}
            await Task.Yield();
        }
        //============================

        shakeAble = false;
        //���͂̎�t�I��
        Debug.Log("shake�I��");
        shakedCount.Add(counter);
        shakeAbleObject.SetActive(false);
        //InputChecker.instance.SetShakeNotAble();
    }
}
