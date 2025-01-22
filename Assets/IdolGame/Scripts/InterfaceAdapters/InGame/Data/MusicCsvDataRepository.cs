using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Entities;
using IdolGame.InGame.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace IdolGame.InGame.Data;

public class MusicCsvDataRepository : IMusicCSVDataRepository
{
    // BPMの値
    int bpm;

    // 高さの値
    int height;

    // 開始高さ
    readonly int startHeight = 5;

    // CSVファイル名を指定して音楽データを非同期で読み込むメソッドの実装
    public async UniTask<List<CSVMusicData>> LoadMusicDataAsync(string? csvFileName, CancellationToken ct)
    {
        List<CSVMusicData> musicDataList = new List<CSVMusicData>();

        // CSV データを Addressables で非同期に読み込む
        AsyncOperationHandle<TextAsset> csvHandle = Addressables.LoadAssetAsync<TextAsset>(csvFileName);
        await csvHandle.Task;
        if (csvHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"CSVファイルが見つかりません: {csvFileName}");
            return musicDataList;
        }


        // 読み込んだCSVファイルをTextAssetとして取得
        TextAsset csvFile = csvHandle.Result;
        
        StringReader reader = new StringReader(csvFile.text);
        List<string[]> csvData = new List<string[]>();
        while (reader.Peek() != -1)
        {
            string? line = reader.ReadLine();
            if (line != null) csvData.Add(line.Split(','));
        }
        
        // BPMを取得
        bpm = int.Parse(csvData[1][3]);
        for (int i = startHeight - 1; i < height; ++i)
        {
            CSVMusicData data = new CSVMusicData
            {
                Time = Convert.ToSingle(csvData[i][0]), // 時間を取得
                TypeOfGroup = int.Parse(csvData[i][2]), // グループの種類を取得
                InfoOfGroup = int.Parse(csvData[i][3]), // グループの情報を取得
                Position = new List<Vector2>() // 位置情報をリストで初期化
            };
            for (int j = 0; j < data.InfoOfGroup; ++j)
            {
                data.Position.Add(new Vector2(int.Parse(csvData[i][j + 4]), 0)); // 位置情報を追加
            }

            musicDataList.Add(data); // 音楽データをリストに追加
        }
        return musicDataList; // 音楽データリストを返す
    }

    // BPMを取得するメソッド
    public int GetBpm()
    {
        return bpm;
    }
}