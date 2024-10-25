using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManagerReon : MonoBehaviour
{
    // シングルトンパターンを使用して、このクラスのインスタンスが一つだけ存在するようにする
    public static GManagerReon instance = null;

    // ゲームのスコアに関する変数
    public float maxScore;     // 最大スコア
    public float ratioScore;   // スコアの割合
    public int songID;         // 曲のID
    public float noteSpeed;    // ノートの速度
    public bool Start;         // ゲームが開始されているかどうかのフラグ
    public float StartTime;    // ゲーム開始時間
    public int combo;          // 現在のコンボ数
    public int score;          // 現在のスコア
    public int perfect;        // パーフェクトの数
    public int great;          // グレートの数
    public int bad;            // バッドの数
    public int miss;           // ミスの数

    public void Awake()
    {
        // インスタンスが存在しない場合、このオブジェクトをインスタンスとして設定し、シーン間で破棄されないようにする
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // すでにインスタンスが存在する場合、このオブジェクトを破棄する
            Destroy(this.gameObject);
        }
    }
}
