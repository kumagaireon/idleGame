namespace IdolGame.InGame.Entities;

public class VideoClip
{
    // ビデオクリップの名前
    public string Name { get; set; }
    // ビデオのパス
    public string VideoPath { get; set; }

    public VideoClip(string name, string videoPath)
    {
        Name = name;
        VideoPath = videoPath;
    }
}