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
    [SerializeField] private float noteScore = 100;
    [Header("パーフェクトノーツスコア")]
    [SerializeField] private float notePerfectScore = 300;
    [Header("タップスコア")]
    [SerializeField] private float tapScore = 200;
    [Header("サイリウムスコア")]
    [SerializeField] private float PsylliumScore = 100;
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
                    maxScore += 300f;

                    //個数分を加算
                    int num = CSVReader.data[i].InfoOfGroup;
                    maxScore += 100 * num;

                    break;
                case 1:
                    //1グループにつき３回タップできるとする
                    maxScore += 200 * 3;
                    break;
            }
        }
        maxScoreText.text = "最大スコア：" + maxScore.ToString();
        ShowRatio();
    }

    public void GetNoteScore()
    {
        score += noteScore;
        scoreText.text = "スコア：" + score.ToString();
        ShowRatio();
    }

    public void GetNotePerfectScore()
    {
        score += notePerfectScore;
        scoreText.text = "スコア：" + score.ToString();
        ShowRatio();
    }

    public void GetTapScore()
    {
        score += tapScore;
        scoreText.text = "スコア：" + score.ToString();
        ShowRatio();
    }

    public void GetPsylliumScore()
    {
        score += PsylliumScore;
        scoreText.text = "スコア：" + score.ToString();
        ShowRatio();
    }

    private void ShowRatio()
    {
        float ratio = (score / maxScore) * 100;
        ratioText.text = "盛り上がり度：" + ratio.ToString() + "%";
    }
}
