namespace IdolGame.InGame.Interfaces;

public interface IScoreRepository
{
    // ノートスコアを追加するメソッド
    void AddNoteScore();
    
    // パーフェクトノートスコアを追加するメソッド
    void AddPerfectNoteScore();
    
    // タップスコアを追加するメソッド
    void AddTapScore();
    
    // Psylliumスコアを追加するメソッド
    void AddPsylliumScore();
    
    // パーフェクトノートスコアを減算するメソッド
    void SubtractNoteScore();
    
    // タップスコアを減算するメソッド
    void SubtractPerfectNoteScore();
    
    // タップスコアを減算するメソッド
    void SubtractTapScore();
    
    // Psylliumスコアを減算するメソッド
    void SubtractPsylliumScore();
    
    // 現在のスコアを取得するメソッド
    float GetCurrentScore();
    
    // 最大スコアを取得するメソッド
    float GetMaxScore();
}