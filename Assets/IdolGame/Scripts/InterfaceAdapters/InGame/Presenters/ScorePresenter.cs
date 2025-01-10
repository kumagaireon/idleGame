using UnityEngine.UI;

namespace IdolGame.InGame.Presenters;

public static class ScorePresenter
{
    private static Text _scoreText;
    private static Text _maxScoreText;
    private static Text _ratioText;

    public static void Initialize(Text scoreText, Text maxScoreText, Text ratioText)
    {
        _scoreText = scoreText;
        _maxScoreText = maxScoreText;
        _ratioText = ratioText;
    }

    public static void UpdateScoreText(float currentScore)
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"スコア: {currentScore}";
        }
    }

    public static void UpdateMaxScoreText(float maxScore)
    {
        if (_maxScoreText != null)
        {
            _maxScoreText.text = $"最大スコア: {maxScore}";
        }
    }

    public static void UpdateRatioText(float ratio)
    {
        if (_ratioText != null)
        {
            _ratioText.text = $"割合: {ratio}%";
        }
    }
}