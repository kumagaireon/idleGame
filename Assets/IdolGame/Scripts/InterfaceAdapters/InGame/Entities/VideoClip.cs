namespace IdolGame.InGame.Entities;

public class VideoClip
{
    public string Name { get; set; }
    public string VideoPath { get; set; }

    public VideoClip(string name, string videoPath)
    {
        Name = name;
        VideoPath = videoPath;
    }
}