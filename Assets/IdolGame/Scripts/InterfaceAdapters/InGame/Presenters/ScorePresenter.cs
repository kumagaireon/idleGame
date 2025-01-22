using UnityEngine.UI;

namespace IdolGame.InGame.Presenters;

public static class ScorePresenter
{
    private static Text _scoreText;// スコアを表示するText
    private static Text _maxScoreText;// 最大スコアを表示するText
    private static Text _ratioText;// スコアの割合を表示するText

    public static void Initialize(Text scoreText, Text maxScoreText, Text ratioText)
    {
        _scoreText = scoreText;
        _maxScoreText = maxScoreText;
        _ratioText = ratioText;
    }

    // スコアテキストを更新するメソッド
    public static void UpdateScoreText(float currentScore)
    {
        if (_scoreText != null)
        {
            // 現在のスコアを表示
            _scoreText.text = $"スコア: {currentScore}";
        }
    }

    // 最大スコアテキストを更新するメソッド
    public static void UpdateMaxScoreText(float maxScore)
    {
        if (_maxScoreText != null)
        {
            // 最大スコアを表示
            _maxScoreText.text = $"最大スコア: {maxScore}";
        }
    }

    // スコアの割合テキストを更新するメソッド
    public static void UpdateRatioText(float ratio)
    {
        if (_ratioText != null)
        {
            // スコアの割合を表示
            _ratioText.text = $"割合: {ratio}%";
        }
    }
}