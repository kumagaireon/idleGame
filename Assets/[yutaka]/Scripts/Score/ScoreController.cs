using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    int score = 0;
    [Header("�m�[�c�X�R�A")]
    [SerializeField] private int noteScore = 100;
    [Header("�p�[�t�F�N�g�m�[�c�X�R�A")]
    [SerializeField] private int notePerfectScore = 300;
    [Header("�^�b�v�X�R�A")]
    [SerializeField] private int tapScore = 200;
    [Header("�T�C���E���X�R�A")]
    [SerializeField] private int PsylliumScore = 100;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("dij");
    }

    public void GetNoteScore()
    {
        score += noteScore;
        Debug.Log("normal:" + score);
    }

    public void GetNotePerfectScore()
    {
        score += notePerfectScore;
        Debug.Log("Perfect:" + score);
    }

    public void GetTapScore()
    {
        score += tapScore;
        Debug.Log("Score:" + score);
    }

    public void GetPsylliumScore()
    {
        score += PsylliumScore;
        Debug.Log("Score:" + score);
    }
}
