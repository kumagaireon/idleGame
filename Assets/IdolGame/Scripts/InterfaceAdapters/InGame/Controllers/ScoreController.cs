using IdolGame.InGame.Data;
using IdolGame.InGame.UseCases;
using UnityEngine;
using UnityEngine.UI;

namespace IdolGame.InGame.Controllers
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Text maxScoreText;
        [SerializeField] private Text ratioText;
        private ScoreUseCase _scoreUseCase;

        private void Start()
        {
            _scoreUseCase = new ScoreUseCase(ScoreRepository.Instance);
            UpdateUI();
        }

        // スコアを更新するメソッド
        public void UpdateScore(int scoreType)
        {
            _scoreUseCase.UpdateScore(scoreType);
            UpdateUI();
        }

        // UIを更新するメソッド
        private void UpdateUI()
        {
            // 現在のスコアを取得
            float currentScore = _scoreUseCase.GetCurrentScore();
            // 最大スコアを取得
            float maxScore = _scoreUseCase.GetMaxScore();
            // 割合を計算
            float ratio = currentScore / maxScore * 100;
            
            // スコアテキストに現在のスコアを設定
            scoreText.text = $"スコア: {currentScore}";
            // 最大スコアテキストに最大スコアを設定
            maxScoreText.text = $"最大スコア: {maxScore}";
            // 割合テキストに割合を設定
            ratioText.text = $"割合: {ratio}%";
        }
    }
}