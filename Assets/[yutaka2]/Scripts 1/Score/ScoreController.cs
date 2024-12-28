using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CSVReader;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    float score = 0;
    float maxScore = 0;
    [Header("�m�[�c�X�R�A")]
    [SerializeField] private float noteScore = 100;
    [Header("�p�[�t�F�N�g�m�[�c�X�R�A")]
    [SerializeField] private float notePerfectScore = 300;
    [Header("�^�b�v�X�R�A")]
    [SerializeField] private float tapScore = 200;
    [Header("�T�C���E���X�R�A")]
    [SerializeField] private float PsylliumScore = 100;
    [Header("���X�R�A�e�L�X�g")]
    [SerializeField] private Text scoreText;
    [Header("�ő�X�R�A�e�L�X�g")]
    [SerializeField] private Text maxScoreText;
    [Header("�p�[�Z���e�[�W�e�L�X�g")]
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
                    //�p�[�t�F�N�g�������Z
                    maxScore += 300f;

                    //���������Z
                    int num = CSVReader.data[i].InfoOfGroup;
                    maxScore += 100 * num;

                    break;
                case 1:
                    //1�O���[�v�ɂ��R��^�b�v�ł���Ƃ���
                    maxScore += 200 * 3;
                    break;
            }
        }
        maxScoreText.text = "�ő�X�R�A�F" + maxScore.ToString();
        ShowRatio();
    }

    public void GetNoteScore()
    {
        score += noteScore;
        scoreText.text = "�X�R�A�F" + score.ToString();
        ShowRatio();
    }

    public void GetNotePerfectScore()
    {
        score += notePerfectScore;
        scoreText.text = "�X�R�A�F" + score.ToString();
        ShowRatio();
    }

    public void GetTapScore()
    {
        score += tapScore;
        scoreText.text = "�X�R�A�F" + score.ToString();
        ShowRatio();
    }

    public void GetPsylliumScore()
    {
        score += PsylliumScore;
        scoreText.text = "�X�R�A�F" + score.ToString();
        ShowRatio();
    }

    private void ShowRatio()
    {
        float ratio = (score / maxScore) * 100;
        ratioText.text = "����オ��x�F" + ratio.ToString() + "%";
    }
}