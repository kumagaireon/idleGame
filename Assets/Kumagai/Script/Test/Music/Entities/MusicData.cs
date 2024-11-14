namespace Kumagai.Entities
{
    public struct MusicData//バリューオブジェクト化
    {
        public float time;//再生時間
        public float keepTime;//継続時間
        public int direction;//方向(1:上下、2:左右)
        public bool type; // タイプ (true:単, false:長)
    }
}