using System;
using IdolGame.InGame.Interfaces;
using IdolGame.InGame.Presenters;

namespace IdolGame.InGame.UseCases;

public class ScoreUseCase
{
    private readonly IScoreRepository _scoreRepository;

    public ScoreUseCase(IScoreRepository scoreRepository)
    {
        _scoreRepository = scoreRepository;
    }

    public void UpdateScore(int scoreType)
    {
        switch (scoreType)
        {
            case 0: _scoreRepository.AddNoteScore(); break;
            case 1: _scoreRepository.AddPerfectNoteScore(); break;
            case 2: _scoreRepository.AddTapScore(); break;
            case 3: _scoreRepository.AddPsylliumScore(); break;
            default: throw new ArgumentOutOfRangeException(nameof(scoreType), scoreType, null);
        }
    }

    public float GetCurrentScore()
    {
        return _scoreRepository.GetCurrentScore();
    }

    public float GetMaxScore()
    {
        return _scoreRepository.GetMaxScore();
    }
}