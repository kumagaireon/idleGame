using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NoteShaker : MonoBehaviour
{
    private List<int> shakedCount = new List<int>();

    [Header("タップ可能オブジェクト")]
    [SerializeField] private GameObject shakeAbleObject;

    public static bool shakeAble = false;

    //タップ可能時間
    public async Task OnShakeAble()
    {
        
        //入力の受付開始
        //InputChecker.instance.SetShakeAble();
        shakeAbleObject.SetActive(true);
        shakeAble = true;

        //=====入力の回数カウント=====
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
        //入力の受付終了
        Debug.Log("shake終了");
        shakedCount.Add(counter);
        shakeAbleObject.SetActive(false);
        //InputChecker.instance.SetShakeNotAble();
    }
}
