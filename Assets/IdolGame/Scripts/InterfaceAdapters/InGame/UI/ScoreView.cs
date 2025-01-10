using UnityEngine;
using UnityEngine.UI;

namespace IdolGame.InGame.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Text maxScoreText;
        [SerializeField] private Text ratioText;

        public void UpdateScore(float currentScore, float maxScore)
        {
            if (scoreText != null)
            {
                scoreText.text = $"スコア: {currentScore}";
            }

            if (maxScoreText != null)
            {
                maxScoreText.text = $"最大スコア: {maxScore}";
            }

            if (ratioText != null)
            {
                float ratio = currentScore / maxScore * 100;
                ratioText.text = $"割合: {ratio}%";
            }
        }
    }
}