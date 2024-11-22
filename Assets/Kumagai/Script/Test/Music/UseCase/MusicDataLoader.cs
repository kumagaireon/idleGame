using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Kumagai.Entities;
using UnityEngine;

namespace Kumagai.UseCase
{

    public class MusicDataLoader : IMusicDataLoader
    {
        /// <summary>
        /// 指定されたCSVファイルから音楽データを非同期に読み込む
        /// </summary>
        /// <param name="csvFileName">読み込むCSVファイルの名前</param>
        /// <returns>読み込まれたMusicDataKariオブジェクトのリスト</returns>
        public async UniTask<List<MusicDataKari>> LoadMusicDataAsync(string csvFileName)
        {
            // MusicDataKariオブジェクトのリストを初期化
            List<MusicDataKari> dat_list = new List<MusicDataKari>();
            // Resourcesフォルダから指定されたCSVファイルを非同期にロード
            TextAsset csvFile = await Resources.LoadAsync<TextAsset>($"CSV/{csvFileName}") as TextAsset;
            // CSVファイルが見つからない場合、空のリストを返す
            if (csvFile == null) return dat_list;

            // CSVファイルのテキストを読み込むためのStringReaderを初期化
            StringReader reader = new StringReader(csvFile.text);
            // CSVデータを格納するリストを初期化
            List<string[]> csvData = new List<string[]>();
            int height = 0;

            // CSVファイルの全行を読み込み、リストに追加
            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                csvData.Add(line.Split(','));
                height++;
            }

            // CSVデータの各行をMusicDataKariオブジェクトに変換し、リストに追
            for (int i = 3; i < height; ++i)
            {
                var dat = new MusicDataKari
                {
                    time = float.Parse(csvData[i][0]), // 時間を設定
                    keepTime = float.Parse(csvData[i][1]), // 維持時間を設定
                    direction = int.Parse(csvData[i][2]), // 方向を設定
                    type = bool.Parse(csvData[i][3]) // 種類を設定 };
                };
                dat_list.Add(dat);
            }

            // 読み込んだMusicDataKariオブジェクトのリストを返す
            return dat_list;
        }
    }
}