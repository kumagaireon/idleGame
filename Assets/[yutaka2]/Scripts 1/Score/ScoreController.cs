using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CSVReader;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    float score = 0;
    float maxScore = 0;

    [Header("ノートスコア")] [SerializeField] private float noteScore = 200;

    [Header("パーフェクトノートスコア")] [SerializeField]
    private float notePerfectScore = 500;

    [Header("タップスコア")] [SerializeField] private float tapScore = 200;
    [Header("サイリウムスコア")] [SerializeField] private float psylliumScore = 100;

    [Header("スコアテキスト")] [SerializeField] private Text scoreText;
    [Header("最大スコアテキスト")] [SerializeField] private Text maxScoreText;

    [Header("パーセンテージテキスト")] [SerializeField]
    private Text ratioText;


    private List<MusicData> data = new List<MusicData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 最大スコアを計算し、スコアを初期化
        CalculateMaxScore();
        score = maxScore / 2;
    }

    // 最大スコアを計算するメソッド
    public void CalculateMaxScore()
    {
        Debug.Log(CSVReader.data.Count);

        for (int i = 0; i < CSVReader.data.Count; i++)
        {
            switch (CSVReader.data[i].TypeOfGroup)
            {
                case 0:
                    // パーフェクトノートスコアを追加
                    maxScore += notePerfectScore;

                    // ノートの数だけスコアを追加
                    int num = CSVReader.data[i].InfoOfGroup;
                    maxScore += noteScore * num;

                    break;
                case 1:
                    // タップスコアを追加
                    maxScore += tapScore;
                    break;
                case 2:
                    // サイリウムスコアを追加
                    maxScore += psylliumScore * 3;
                    break;
            }
        }

        // 最大スコアをテキストに表示
        maxScoreText.text = "最大スコア: " + maxScore.ToString();
        ShowRatio();
    }

    // ノートスコアを加算するメソッド
    public void GetNoteScore()
    {
        score += noteScore;
        UpdateScoreText();
        ShowRatio();
    }

    // パーフェクトノートスコアを加算するメソッド
    public void GetNotePerfectScore()
    {
        score += notePerfectScore;
        UpdateScoreText();
        ShowRatio();
    }

    // タップスコアを加算するメソッド
    public void GetTapScore()
    {
        score += tapScore;
        UpdateScoreText();
        ShowRatio();
    }

    // サイリウムスコアを加算するメソッド
    public void GetPsylliumScore()
    {
        score += psylliumScore;
        UpdateScoreText();
        ShowRatio();
    }

    // 現在のスコアの割合を表示するメソッド
    private void ShowRatio()
    {
        float ratio = (score / maxScore) * 100;
        string rank;
        if (ratio >= 90.0f)
        {
            rank = "S";
        }
        else if (ratio >= 60.0f)
        {
            rank = "A";
        }
        else if (ratio >= 30.0f)
        {
            rank = "B";
        }
        else
        {
            rank = "C";
        }

        ratioText.text = rank;
    }

    // ノートスコアを減算するメソッド
    public void MinusNoteScore()
    {
        score -= noteScore;
        UpdateScoreText();
        ShowRatio();
    }

    // タップスコアを減算するメソッド
    public void MinusTapScore()
    {
        score -= 100;
        UpdateScoreText();
        ShowRatio();
    }

    // スコアテキストを更新するメソッド
    public void UpdateScoreText()
    {
        scoreText.text = "スコア: " + score.ToString();
    }
}
