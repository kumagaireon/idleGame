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
            _scoreUseCase = new ScoreUseCase(new ScoreRepository());
            UpdateUI();
        }

        public void UpdateScore(int scoreType)
        {
            _scoreUseCase.UpdateScore(scoreType);
            UpdateUI();
        }

        private void UpdateUI()
        {
            float currentScore = _scoreUseCase.GetCurrentScore();
            float maxScore = _scoreUseCase.GetMaxScore();
            float ratio = currentScore / maxScore * 100;
            scoreText.text = $"スコア: {currentScore}";
            maxScoreText.text = $"最大スコア: {maxScore}";
            ratioText.text = $"割合: {ratio}%";
        }
    }
}