using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Kumagai.Entities;

namespace Kumagai.UseCase
{
    public interface IMusicDataLoader
    {
        UniTask<List<MusicDataKari>> LoadMusicDataAsync(string csvFileName);
    }
}