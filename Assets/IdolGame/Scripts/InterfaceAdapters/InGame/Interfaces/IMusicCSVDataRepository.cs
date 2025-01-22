using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Entities;

namespace IdolGame.InGame.Interfaces;

public interface IMusicCSVDataRepository
{
    // CSVファイル名を指定して音楽データを非同期で取得するメソッド
    UniTask<List<CSVMusicData>> LoadMusicDataAsync(string? csvFileName, CancellationToken ct);
    // BPM（拍数）を取得するメソッド
    int GetBpm();
}