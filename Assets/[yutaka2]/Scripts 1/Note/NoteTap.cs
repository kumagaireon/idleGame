using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NoteTap : MonoBehaviour
{
    private List<int> tappedCount = new List<int>();

    [Header("�^�b�v�\�I�u�W�F�N�g")]
    [SerializeField] private GameObject[] tapAbleObject;

    [Header("�㏸���x")]
    [SerializeField] private float moveSpeed = 0.3f;

    [Header("����y���W")]
    [SerializeField] private float startPosY = -10.0f;

    [Header("��������")]
    [SerializeField] private float lifeTime = 2.0f;

    //�^�b�v�\����
    public async Task OnTapAble()
    {
        int index = 0;

        //���͂̎�t�J�n
        //InputChecker.instance.SetTapAble();
        ChangeToStartPosition(index);
        tapAbleObject[index].SetActive(true);        

        //=====���͂̉񐔃J�E���g=====
        int counter = 0;
        float timer = 0;
        while (timer < lifeTime)
        {
            MoveObject(index);
            timer += Time.deltaTime;
            if (InputChecker.instance.TappedEnter())
            {
                counter++;
                ScoreController.instance.GetTapScore();
            }
            await Task.Yield();
        }
        //============================

        //���͂̎�t�I��
        Debug.Log("tap�I�� count:" + counter);
        tappedCount.Add(counter);
        tapAbleObject[index].SetActive(false);
        //InputChecker.instance.SetTapNotAble();
    }

    private int DecideRandom()
    {
        int random = Random.Range(0, tapAbleObject.Length);
        return random;
    }

    private void ChangeToStartPosition(int index)
    {
        float posX = tapAbleObject[index].transform.position.x;
        tapAbleObject[index].transform.position = new Vector2(posX, startPosY);
    }

    private void MoveObject(int index)
    {
        tapAbleObject[index].transform.Translate(Vector2.up * moveSpeed);
    }
}
