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

    private int index = 0;

    private float currentPosX = -5.0f;

    private Vector3 mousePosition;

    public async void StartTabAble()
    {
        await OnTapAble(index);
    }

    //�^�b�v�\����
    public async Task OnTapAble(int playIndex)
    {
        
        index = (index + 1) % 5;

        //���͂̎�t�J�n
        //InputChecker.instance.SetTapAble();
        ChangeToStartPosition(playIndex);
        tapAbleObject[playIndex].SetActive(true);        

        //=====���͂̉񐔃J�E���g=====
        int counter = 0;
        float timer = 0;
        while (timer < lifeTime)
        {
            MoveObject(playIndex);
            timer += Time.deltaTime;
            if (InputChecker.instance.GetTapped())            
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log(mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);                
                if (hit.collider != null)
                {
                    tapAbleObject[playIndex].SetActive(false);
                    ScoreController.instance.GetTapScore();
                }
                else
                {
                    Debug.Log("null");
                }

                    
            }
            await Task.Yield();
        }
        //============================

        //���͂̎�t�I��
        Debug.Log("tap�I�� count:" + counter);
        tappedCount.Add(counter);
        if(tapAbleObject[playIndex].activeSelf)
        {
            ScoreController.instance.MinusTapScore();
            tapAbleObject[playIndex].SetActive(false);
        }
        
        //InputChecker.instance.SetTapNotAble();
    }

    private int DecideRandom()
    {
        int random = Random.Range(0, tapAbleObject.Length);
        return random;
    }

    private void ChangeToStartPosition(int index)
    {
        currentPosX *= -1;
        tapAbleObject[index].transform.position = new Vector2(currentPosX, startPosY);
    }

    private void MoveObject(int index)
    {
        tapAbleObject[index].transform.Translate(Vector2.up * moveSpeed);
    }
}
