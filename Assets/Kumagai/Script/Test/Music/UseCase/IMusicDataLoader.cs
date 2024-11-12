using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Kumagai.Entities;

namespace Kumagai.UseCase
{
    public interface IMusicDataLoader
    {
        UniTask<List<MusicData>> LoadMusicDataAsync(string csvFileName);
    }
}