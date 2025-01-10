namespace IdolGame.InGame.Models;

public class SelectedMusicData
{
    public string? Name { get; set; }
    public string VideoPath { get; set; }
    public string? CsvPath { get; set; }
    public float Score { get; set; }

    public SelectedMusicData(string? name, string videoPath, string? csvPath, float score)
    {
        Name = name;
        VideoPath = videoPath;
        CsvPath = csvPath;
        Score = score;
    }
}