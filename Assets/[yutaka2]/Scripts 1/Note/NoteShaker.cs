using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NoteShaker : MonoBehaviour
{
    // シェイクのカウントリスト
    private List<int> shakedCount = new List<int>();

    [Header("タップ可能オブジェクト")] [SerializeField]
    private GameObject shakeAbleObject;

    // シェイクが可能かどうかを示すフラグ
    public static bool shakeAble = false;

    // 指定されたオブジェクトをシェイク可能にする
    public async Task OnShakeAble()
    {

        // シェイク可能状態を設定
        //InputChecker.instance.SetShakeAble();
        shakeAbleObject.SetActive(true);
        shakeAble = true;

        // シェイクのカウント
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

        // シェイク終了
        shakeAble = false;
        Debug.Log("shake完了");
        shakedCount.Add(counter);
        shakeAbleObject.SetActive(false);
        //InputChecker.instance.SetShakeNotAble();
    }
}