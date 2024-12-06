using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NoteTap : MonoBehaviour
{
    private List<int> tappedCount = new List<int>();

    [Header("タップ可能オブジェクト")]
    [SerializeField] private GameObject tapAbleObject;

    //タップ可能時間
    public async Task OnTapAble()
    {
        //入力の受付開始
        //InputChecker.instance.SetTapAble();
        tapAbleObject.SetActive(true);

        //=====入力の回数カウント=====
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

        //入力の受付終了
        Debug.Log("tap終了 count:" + counter);
        tappedCount.Add(counter);
        tapAbleObject.SetActive(false);
        //InputChecker.instance.SetTapNotAble();
    }
}
