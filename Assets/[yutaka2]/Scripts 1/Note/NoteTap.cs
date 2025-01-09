using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NoteTap : MonoBehaviour
{
    // タップのカウントリスト
    private List<int> tappedCount = new List<int>();

    [Header("タップ可能オブジェクト")]
    [SerializeField] private GameObject[] tapAbleObject; 
   
    [Header("移動速度")] 
    [SerializeField] private float moveSpeed = 0.3f; 
    
    [Header("開始位置のY座標")] 
    [SerializeField] private float startPosY = -10.0f;
    
    [Header("寿命時間")] 
    [SerializeField] private float lifeTime = 2.0f;

    private int index = 0;// 現在のインデックス

    private float currentPosX = -5.0f;// 現在のX座標

    private Vector3 mousePosition;// マウスの位置

    // タップ可能オブジェクトの動作を開始
    public async void StartTabAble()
    {
        await OnTapAble(index);
    }

    // 指定されたインデックスのオブジェクトをタップ可能にする
    public async UniTask OnTapAble(int playIndex)
    {
        // インデックスを更新（5つのオブジェクトを循環）
        index = (index + 1) % 5;

        // タップ可能状態を設定
        //InputChecker.instance.SetTapAble();
        ChangeToStartPosition(playIndex);
        tapAbleObject[playIndex].SetActive(true);        

        // タップカウント
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
                // マウス位置でのRaycast
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);                
                if (hit.collider != null)
                {
                    // オブジェクトがタップされた場合
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

        // タップ終了
        Debug.Log("tap完了 count:" + counter);
        tappedCount.Add(counter);
        if(tapAbleObject[playIndex].activeSelf)
        {
            ScoreController.instance.MinusTapScore();
            tapAbleObject[playIndex].SetActive(false);
        }
        
        //InputChecker.instance.SetTapNotAble();
    }

    // ランダムなインデックスを決定
    private int DecideRandom()
    {
        int random = Random.Range(0, tapAbleObject.Length);
        return random;
    }

    // オブジェクトの開始位置を設定
    private void ChangeToStartPosition(int index)
    {
        currentPosX *= -1;
        tapAbleObject[index].transform.position = new Vector2(currentPosX, startPosY);
    }

    // オブジェクトを移動
    private void MoveObject(int index)
    {
        tapAbleObject[index].transform.Translate(Vector2.up * moveSpeed);
    }
}
