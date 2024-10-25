using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour
{
    // 変数の宣言
    [SerializeField] private GameObject[] MessageObj; // プレイヤーに判定を伝えるゲームオブジェクトの配列
    [SerializeField] NotesManager notesManager; // NotesManagerスクリプトを格納する変数
    [SerializeField] TextMeshProUGUI comboText; // コンボ数を表示するTextMeshProUGUIオブジェクト
    [SerializeField] TextMeshProUGUI scoreText; // スコアを表示するTextMeshProUGUIオブジェクト
    [SerializeField] GameObject finish; // ゲーム終了時に表示するゲームオブジェクト
    private AudioSource audio; // オーディオソースコンポーネント
    [SerializeField] AudioClip hitSound; // ノーツを叩いた時の効果音
    float endTime = 0; // ゲームの終了時間

    private void Start()
    {
        // オーディオソースコンポーネントを取得
        audio = GetComponent<AudioSource>();
        // 最後のノーツの時間を取得し、ゲームの終了時間を設定
        endTime = notesManager.NotesTime[notesManager.NotesTime.Count - 1];
    }

    void Update()
    {
        // ゲームが開始されている場合
        if (GManagerReon.instance.Start)
        {
            // キー入力に応じたノーツの判定
            if (Input.GetKeyDown(KeyCode.D)) // "D"キーが押されたとき
            {
                if (notesManager.LaneNum[0] == 0) // レーンの番号が一致するか確認
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManagerReon.instance.StartTime)), 0);
                }
                else if (notesManager.LaneNum[1] == 0)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManagerReon.instance.StartTime)), 1);
                }
            }

            if (Input.GetKeyDown(KeyCode.F)) // "F"キーが押されたとき
            {
                if (notesManager.LaneNum[0] == 1)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManagerReon.instance.StartTime)), 0);
                }
                else if (notesManager.LaneNum[1] == 1)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManagerReon.instance.StartTime)), 1);
                }
            }

            if (Input.GetKeyDown(KeyCode.J)) // "J"キーが押されたとき
            {
                if (notesManager.LaneNum[0] == 2)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManagerReon.instance.StartTime)), 0);
                }
                else if (notesManager.LaneNum[1] == 2)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManagerReon.instance.StartTime)), 1);
                }
            }

            if (Input.GetKeyDown(KeyCode.K)) // "K"キーが押されたとき
            {
                if (notesManager.LaneNum[0] == 3)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManagerReon.instance.StartTime)), 0);
                }
                else if (notesManager.LaneNum[1] == 3)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManagerReon.instance.StartTime)), 1);
                }
            }

            // 最後のノーツを叩いた後の処理
            if (Time.time > endTime + GManagerReon.instance.StartTime)
            {
                finish.SetActive(true); // ゲーム終了オブジェクトを表示
                Invoke("ResuleScene", 3f); // 3秒後に結果画面に遷移
                return;
            }

            // ノーツを叩くべき時間から0.2秒経過しても入力がなかった場合
            if (Time.time > notesManager.NotesTime[0] + 0.2f + GManagerReon.instance.StartTime)
            {
                message(3); // ミスメッセージを表示
                deleteData(0); // ノーツデータを削除
                Debug.Log("Miss"); // デバッグログに"Miss"を表示
                GManagerReon.instance.miss++; // ミスカウントを増加
                GManagerReon.instance.combo = 0; // コンボをリセット
            }
        }
    }

    // ノーツの判定処理
    void Judgement(float timeLag, int numOffset)
    {
        audio.PlayOneShot(hitSound); // 効果音を再生
        if (timeLag <= 0.05) // 判定時間が0.05秒以下の場合
        {
            Debug.Log("Perfect"); // デバッグログに"Perfect"を表示
            message(0); // パーフェクトメッセージを表示
            GManagerReon.instance.ratioScore += 5; // スコアを加算
            GManagerReon.instance.perfect++; // パーフェクトカウントを増加
            GManagerReon.instance.combo++; // コンボを増加
            deleteData(numOffset); // ノーツデータを削除
        }
        else if (timeLag <= 0.08) // 判定時間が0.08秒以下の場合
        {
            Debug.Log("Great"); // デバッグログに"Great"を表示
            message(1); // グレートメッセージを表示
            GManagerReon.instance.ratioScore += 3; // スコアを加算
            GManagerReon.instance.great++; // グレートカウントを増加
            GManagerReon.instance.combo++; // コンボを増加
            deleteData(numOffset); // ノーツデータを削除
        }
        else if (timeLag <= 0.10) // 判定時間が0.10秒以下の場合
        {
            Debug.Log("Bad"); // デバッグログに"Bad"を表示
            message(2); // バッドメッセージを表示
            GManagerReon.instance.ratioScore += 1; // スコアを加算
            GManagerReon.instance.bad++; // バッドカウントを増加
            GManagerReon.instance.combo = 0; // コンボをリセット
            deleteData(numOffset); // ノーツデータを削除
        }
    }

    // 引数の絶対値を返す関数
    float GetABS(float num)
    {
        if (num >= 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
    }

    // すでに叩いたノーツを削除する関数
    void deleteData(int numOffset)
    {
        notesManager.NotesTime.RemoveAt(numOffset); // ノーツ時間データを削除
        notesManager.LaneNum.RemoveAt(numOffset); // レーン番号データを削除
        notesManager.NoteType.RemoveAt(numOffset); // ノーツタイプデータを削除
        GManagerReon.instance.score = (int)Math.Round(1000000 * Math.Floor(
            GManagerReon.instance.ratioScore / GManagerReon.instance.maxScore * 1000000) / 1000000); // スコアを計算して更新
        comboText.text = GManagerReon.instance.combo.ToString(); // コンボ数を更新
        scoreText.text = GManagerReon.instance.score.ToString(); // スコアを更新
    }

    // 判定を表示する関数
    void message(int judge)
    {
        Instantiate(MessageObj[judge],
            new Vector3(notesManager.LaneNum[0] - 1.5f, 0.76f, 0.15f),
            Quaternion.Euler(45, 0, 0));
    }

    // 結果画面に遷移する関数
    private void ResuleScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}