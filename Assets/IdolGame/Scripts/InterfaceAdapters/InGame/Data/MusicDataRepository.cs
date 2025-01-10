using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Entities;
using IdolGame.InGame.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace IdolGame.InGame.Data;

public class MusicDataRepository : MonoBehaviour, IMusicDataRepository
{
    private int bpm;
    private int height;
    private readonly int startHeight = 5;

    public async UniTask<List<CSVMusicData>> LoadMusicDataAsync(string csvFileName)
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


        TextAsset csvFile = csvHandle.Result;
        StringReader reader = new StringReader(csvFile.text);
        List<string[]> csvData = new List<string[]>();
        while (reader.Peek() > -1)
        {
            string? line = reader.ReadLine();
            if (line != null) csvData.Add(line.Split(','));
            height++;
        }

        bpm = int.Parse(csvData[1][3]);
        for (int i = startHeight - 1; i < height; ++i)
        {
            CSVMusicData data = new CSVMusicData
            {
                Time = Convert.ToSingle(csvData[i][0]), TypeOfGroup = int.Parse(csvData[i][2]),
                InfoOfGroup = int.Parse(csvData[i][3]), Position = new List<int>()
            };
            for (int j = 0; j < data.InfoOfGroup; ++j)
            {
                data.Position.Add(int.Parse(csvData[i][j + 4]));
            }

            musicDataList.Add(data);
        }

        return musicDataList;
    }

    public int GetBpm()
    {
        return bpm;
    }
}