namespace IdolGame.InGame.Interfaces;

public interface IScoreRepository
{
    void AddNoteScore();
    void AddPerfectNoteScore();
    void AddTapScore();
    void AddPsylliumScore();
    void SubtractNoteScore();
    void SubtractPerfectNoteScore();
    void SubtractTapScore();
    void SubtractPsylliumScore();
    float GetCurrentScore();
    float GetMaxScore();
}