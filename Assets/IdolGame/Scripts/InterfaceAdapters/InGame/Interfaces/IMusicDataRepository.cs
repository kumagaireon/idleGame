using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Entities;

namespace IdolGame.InGame.Interfaces;

public interface IMusicDataRepository
{
    UniTask<List<CSVMusicData>> LoadMusicDataAsync(string csvFileName);
    int GetBpm();
}