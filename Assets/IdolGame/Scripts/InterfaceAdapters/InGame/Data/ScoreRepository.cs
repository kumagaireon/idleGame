using IdolGame.InGame.Interfaces;

namespace IdolGame.InGame.Data;

public class ScoreRepository : IScoreRepository
{
    private float _currentScore = 0;
    private float _maxScore = 1000; // サンプルの最大スコア

    public void AddNoteScore()
    {
        _currentScore += 200; // サンプルのノートスコア
        if (_currentScore > _maxScore) _currentScore = _maxScore; // 最大スコアを超えないようにする
    }

    public void AddPerfectNoteScore()
    {
        _currentScore += 500; // サンプルのパーフェクトノートスコア
        if (_currentScore > _maxScore) _currentScore = _maxScore; // 最大スコアを超えないようにする
    }

    public void AddTapScore()
    {
        _currentScore += 200; // サンプルのタップスコア
        if (_currentScore > _maxScore) _currentScore = _maxScore; // 最大スコアを超えないようにする
    }

    public void AddPsylliumScore()
    {
        _currentScore += 100; // サンプルのサイリウムスコア
        if (_currentScore > _maxScore) _currentScore = _maxScore; // 最大スコアを超えないようにする
    }

    public void SubtractNoteScore()
    {
        _currentScore -= 200; // サンプルのノートスコア減少
        if (_currentScore < 0) _currentScore = 0; // スコアが0未満にならないようにする
    }

    public void SubtractPerfectNoteScore()
    {
        _currentScore -= 500; // サンプルのパーフェクトノートスコア減少
        if (_currentScore < 0) _currentScore = 0; // スコアが0未満にならないようにする
    }

    public void SubtractTapScore()
    {
        _currentScore -= 200; // サンプルのタップスコア減少
        if (_currentScore < 0) _currentScore = 0; // スコアが0未満にならないようにする
    }

    public void SubtractPsylliumScore()
    {
        _currentScore -= 100; // サンプルのサイリウムスコア減少
        if (_currentScore < 0) _currentScore = 0; // スコアが0未満にならないようにする
    }
    
    public float GetCurrentScore()
    {
        return _currentScore;
    }

    public float GetMaxScore()
    {
        return _maxScore;
    }
}