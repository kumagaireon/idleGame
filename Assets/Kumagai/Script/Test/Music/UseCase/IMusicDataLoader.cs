using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Kumagai.Entities;

namespace Kumagai.UseCase
{
    /// <summary>
    /// 音楽データの読み込みを管理するためのインターフェース
    /// </summary>
    public interface IMusicDataLoader
    {
        /// <summary>
        /// 指定されたCSVファイルから音楽データを非同期に読み込むメソッド
        /// </summary>
        /// <param name="csvFileName">読み込むCSVファイルの名前</param>
        /// <returns>読み込まれたMusicDataKariオブジェクトのリスト</returns>
        UniTask<List<MusicDataKari>> LoadMusicDataAsync(string csvFileName);
    }
}