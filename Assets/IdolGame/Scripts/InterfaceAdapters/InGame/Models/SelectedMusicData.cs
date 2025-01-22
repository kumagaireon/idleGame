namespace IdolGame.InGame.Models;

public class SelectedMusicData
{
    public string Name { get; set; }
    public string VideoPath { get; set; }
    public string CsvPath { get; set; }
    public int Bpm { get; set; }

    public SelectedMusicData(string name, string videoPath, string csvPath, int bpm)
    {
        Name = name;
        VideoPath = videoPath;
        CsvPath = csvPath;
        Bpm = bpm;
    }
}