using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NoteTap : MonoBehaviour
{
    private List<int> tappedCount = new List<int>();

    [Header("タップ可能オブジェクト")]
    [SerializeField] private GameObject[] tapAbleObject;

    [Header("上昇速度")]
    [SerializeField] private float moveSpeed = 0.3f;

    [Header("初期y座標")]
    [SerializeField] private float startPosY = -10.0f;

    [Header("生存時間")]
    [SerializeField] private float lifeTime = 2.0f;

    //タップ可能時間
    public async Task OnTapAble()
    {
        int index = 0;

        //入力の受付開始
        //InputChecker.instance.SetTapAble();
        ChangeToStartPosition(index);
        tapAbleObject[index].SetActive(true);        

        //=====入力の回数カウント=====
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

        //入力の受付終了
        Debug.Log("tap終了 count:" + counter);
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
