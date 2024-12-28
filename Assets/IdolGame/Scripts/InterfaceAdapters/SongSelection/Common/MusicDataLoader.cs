using System.Collections.Generic; 
using System.IO; 
using System.Text.Json;
using System.Threading.Tasks; 
using UnityEngine;
namespace IdolGame.SongSelection;

public class MusicDataLoader
{
    private const string FilePath = "master_data/music_data.json";

    public async Task<List<MusicData>?> LoadMusicDataAsync()
    {
        var path = Path.Combine(Application.streamingAssetsPath, FilePath);
        var json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<List<MusicData>>(json);
    }
}