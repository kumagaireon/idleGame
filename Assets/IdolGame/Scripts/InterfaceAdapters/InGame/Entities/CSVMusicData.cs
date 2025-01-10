using System.Collections.Generic;

namespace IdolGame.InGame.Entities;

public class CSVMusicData
{
    public float Time { get; set; }
    public int TypeOfGroup { get; set; }
    public int InfoOfGroup { get; set; }
    public List<int>? Position { get; set; }
}