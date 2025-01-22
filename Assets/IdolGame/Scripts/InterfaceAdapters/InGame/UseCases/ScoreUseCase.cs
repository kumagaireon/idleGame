using System;
using IdolGame.InGame.Interfaces;
using IdolGame.InGame.Presenters;

namespace IdolGame.InGame.UseCases;

public class ScoreUseCase
{
    private readonly IScoreRepository _scoreRepository;

    // コンストラクタでIScoreRepositoryを受け取る
    public ScoreUseCase(IScoreRepository scoreRepository)
    {
        _scoreRepository = scoreRepository;
    }

    // スコアの種類に応じてスコアを更新するメソッド
    public void UpdateScore(int scoreType)
    {
        if (scoreType == 0)
        {
            _scoreRepository.AddNoteScore();
        }
        else if (scoreType == 1)
        {
            _scoreRepository.AddPerfectNoteScore();
        }
        else if (scoreType == 2)
        {
            _scoreRepository.AddTapScore();
        }
        else if (scoreType == 3)
        {
            _scoreRepository.AddPsylliumScore();
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(scoreType), scoreType, null);
        }
    }

    // 現在のスコアを取得するメソッド
    public float GetCurrentScore()
    {
        return _scoreRepository.GetCurrentScore();
    }

    // 最大スコアを取得するメソッド
    public float GetMaxScore()
    {
        return _scoreRepository.GetMaxScore();
    }
}