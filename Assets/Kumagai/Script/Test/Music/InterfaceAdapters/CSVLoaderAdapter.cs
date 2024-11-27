using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Kumagai.Entities;
using Kumagai.UseCase;
using UnityEngine;

namespace Kumagai.InterfaceAdapters
{
    public class CSVLoaderAdapter : MonoBehaviour
    {
        [SerializeField] private SongDataReon selectedSong; // 選択された音楽データ
        private IMusicDataLoader musicDataLoader;

        public void Initialize(IMusicDataLoader loader)
        {
            musicDataLoader = loader;
            //   Debug.Log("MusicDataLoader initialized in CSVLoaderAdapter");
        }

        public async UniTask LoadMusicData()
        {
            if (musicDataLoader == null)
            {
                //  Debug.LogError("MusicDataLoader が設定されていません。Initialize メソッドを呼び出して設定してください。");
                return;
            }

            // SongDataReonの情報をログに出力
            Debug.Log($"Song ID: {selectedSong.songID}, Song Name: {selectedSong.songName}, Song Level: {selectedSong.songLevel}, CSV File: {selectedSong.csvFileName}");

            // CSVファイルを読み込み
            List<MusicDataKari> data = await musicDataLoader.LoadMusicDataAsync(selectedSong.csvFileName);
            if (DataHolder.Instance != null)
            {
                DataHolder.Instance.SetMusicData(data);
            }
            else
            {
                Debug.LogError("DataHolder が初期化されていません。シーンに DataHolder オブジェクトが存在することを確認してください。");
            }

            // 読み込んだデータのログ出力（必要ならば）
            foreach (var musicData in data)
            {
                // Debug.Log(  $"Time: {musicData.time}, KeepTime: {musicData.keepTime}, Direction: {musicData.direction}, Type: {musicData.type}");
            }
        }
    }
}