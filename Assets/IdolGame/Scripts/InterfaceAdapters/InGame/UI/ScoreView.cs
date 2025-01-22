using UnityEngine;
using UnityEngine.UI;

namespace IdolGame.InGame.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text scoreText;// スコアを表示するText
        [SerializeField] private Text maxScoreText;// 最大スコアを表示するText
        [SerializeField] private Text ratioText;// スコアの割合を表示するText

        // スコアを更新するメソッド
        public void UpdateScore(float currentScore, float maxScore)
        {
            if (scoreText != null)
            {
                // 現在のスコアを表示
                scoreText.text = $"スコア: {currentScore}";
            }

            if (maxScoreText != null)
            {
                // 最大スコアを表示
                maxScoreText.text = $"最大スコア: {maxScore}";
            }

            if (ratioText != null)
            {
                // スコアの割合を計算
                float ratio = currentScore / maxScore * 100;
                // スコアの割合を表示
                ratioText.text = $"割合: {ratio}%";
            }
        }
    }
}