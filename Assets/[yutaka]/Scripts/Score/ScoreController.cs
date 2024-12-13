using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    int score = 0;
    [Header("ノーツスコア")]
    [SerializeField] private int noteScore = 100;
    [Header("パーフェクトノーツスコア")]
    [SerializeField] private int notePerfectScore = 300;
    [Header("タップスコア")]
    [SerializeField] private int tapScore = 200;
    [Header("サイリウムスコア")]
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
