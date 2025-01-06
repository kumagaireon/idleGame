using System.Collections.Generic; 
using System.IO; 
using System.Text.Json;
using System.Threading.Tasks; 
using UnityEngine;

namespace IdolGame.Recommendation;

public class IdolDataLoader
{
    private const string FilePath = "master_data/favorite_idol_data.json";

    public async Task<List<IdolGroupData>?> LoadIdolDataAsync()
    {
        var path = Path.Combine(Application.streamingAssetsPath, FilePath);
        var json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<List<IdolGroupData>>(json);
    }
}