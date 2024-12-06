using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NoteTap : MonoBehaviour
{
    private List<int> tappedCount = new List<int>();

    [Header("�^�b�v�\�I�u�W�F�N�g")]
    [SerializeField] private GameObject tapAbleObject;

    //�^�b�v�\����
    public async Task OnTapAble()
    {
        //���͂̎�t�J�n
        //InputChecker.instance.SetTapAble();
        tapAbleObject.SetActive(true);

        //=====���͂̉񐔃J�E���g=====
        int counter = 0;
        float timer = 0;
        while (timer < 1.0f)
        {
            timer += Time.deltaTime;
            if (InputChecker.instance.TappedEnter())
            {
                counter++;
                Debug.Log(counter);
            }
            await Task.Yield();
        }
        //============================

        //���͂̎�t�I��
        Debug.Log("tap�I�� count:" + counter);
        tappedCount.Add(counter);
        tapAbleObject.SetActive(false);
        //InputChecker.instance.SetTapNotAble();
    }
}
