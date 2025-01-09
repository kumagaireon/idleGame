using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CSVReader;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    float score = 0;
    float maxScore = 0;
    [Header("ノーツスコア")]
    [SerializeField] private float noteScore = 200;
    [Header("パーフェクトノーツスコア")]
    [SerializeField] private float notePerfectScore = 500;
    [Header("タップスコア")]
    [SerializeField] private float tapScore = 200;
    [Header("サイリウムスコア")]
    [SerializeField] private float psylliumScore = 100;
    [Header("現スコアテキスト")]
    [SerializeField] private Text scoreText;
    [Header("最大スコアテキスト")]
    [SerializeField] private Text maxScoreText;
    [Header("パーセンテージテキスト")]
    [SerializeField] private Text ratioText;

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
        CalculateMaxScore();
        score = maxScore / 2;
    }

    public void CalculateMaxScore()
    {          
        Debug.Log(CSVReader.data.Count);
        
        for(int i = 0; i <  CSVReader.data.Count; i++)
        {
            switch (CSVReader.data[i].TypeOfGroup)
            {
                case 0:
                    //パーフェクト分を加算
                    maxScore += notePerfectScore;

                    //個数分を加算
                    int num = CSVReader.data[i].InfoOfGroup;
                    maxScore += noteScore * num;

                    break;
                case 1:
                    //1グループにつき３回タップできるとする
                    maxScore += tapScore;
                    break;
                case 2:
                    maxScore += psylliumScore * 3;
                    break;
            }
        }
        maxScoreText.text = "最大スコア：" + maxScore.ToString();
        ShowRatio();
    }

    public void GetNoteScore()
    {
        score += noteScore;
        UpdateScoreText();
        ShowRatio();
    }

    public void GetNotePerfectScore()
    {
        score += notePerfectScore;
        UpdateScoreText();
        ShowRatio();
    }

    public void GetTapScore()
    {
        score += tapScore;
        UpdateScoreText();
        ShowRatio();
    }

    public void GetPsylliumScore()
    {
        score += psylliumScore;
        UpdateScoreText();
        ShowRatio();
    }

    private void ShowRatio()
    {
        float ratio = (score / maxScore) * 100;
        string rank;
        if(ratio >= 90.0f)
        {
            rank = "S";
        }
        else if (ratio >= 60.0f)
        {
            rank = "A";
        }
        else if(ratio >= 30.0f)
        {
            rank = "B";
        }
        else
        {
            rank = "C";
        }
        ratioText.text = rank;
    }

    public void MinusNoteScore()
    {
        score -= noteScore;
        UpdateScoreText();
        ShowRatio();
    }

    public void MinusTapScore()
    {
        score -= 100;
        UpdateScoreText();
        ShowRatio();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "スコア：" + score.ToString();
    }
}
