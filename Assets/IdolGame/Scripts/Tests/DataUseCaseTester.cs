using System;
using System.Collections;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;
using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.EnterpriseBusinessRules;
using IdolGame.Frameworks;
using UnityEngine;
using UnityEngine.TestTools;

public sealed class DataUseCaseTester
{
    /*
    [UnityTest]
    public IEnumerator TestCreateMusicSelectData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "music_data.json");
        var dataStore = new JsonAsyncDataStore<MusicData[]>(path);
        await dataStore.StoreAsync(new[]
        {
            new MusicData()
            {
                Id = 1,
                Name = "ここに曲名1",
                ImagePath = "ここに画像のパス2",
                Description = "ここに曲の説明文を書いて3"
            }
        }, cts.Token);
    });

    /// <summary>
    /// セーブデータをロードするテスト
    /// </summary>
    [UnityTest]
    public IEnumerator TestLoadMusicSelectData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();
        // JSONデータファイルのパスを構築
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "music_data.json");

        var dataStore = new JsonAsyncDataStore<MusicData[]>(path);
        // データストアからセーブデータを非同期にロード
        var saves = await dataStore.LoadAsync(cts.Token);
        // ロードしたデータをデバッグログに出力
        for (var i = 0; i < saves.Length; i++)
        {
            Debug.Log(saves[i]);
        }
    });

    /// <summary>
    /// セーブデータを見つけるユースケースのテスト
    /// </summary>
    [UnityTest]
    public IEnumerator TestFindMusicSelectDataUscCase() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();
        // JSONデータファイルのパスを構築
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "music_data.json");

        IAsyncDataStore<MusicData[]> dataStore = new JsonAsyncDataStore<MusicData[]>(path);
        // 非同期リポジトリのインスタンスを作成
        IAsyncRepository<MusicData, MusicId> repository = new MusicDataRepository(dataStore);
        var useCase = new FindMusicDataUseCase(repository);
        // ユースケースから全データを非同期に取得
        var saves = await useCase.FindAllAsync(cts.Token);
        // 取得したデータをデバッグログに出力
        foreach (var save in saves)
        {
            Debug.Log(save);
        }
    });
    
    [UnityTest]
    public IEnumerator TestCreateVideoData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "video_data.json");
        var dataStore = new JsonAsyncDataStore<LiveData[]>(path);
        await dataStore.StoreAsync(new[]
        {
            new LiveData()
            {
                VideoId = 1,
                NotesID = 1,
                SoundID = 1,
                CallID = 1
            }
        }, cts.Token);
    });
    
    [UnityTest]
    public IEnumerator TestLoadVideoData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();
        // JSONデータファイルのパスを構築
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "video_data.json");

        var dataStore = new JsonAsyncDataStore<LiveData[]>(path);
        // データストアからセーブデータを非同期にロード
        var saves = await dataStore.LoadAsync(cts.Token);
        // ロードしたデータをデバッグログに出力
        for (var i = 0; i < saves.Length; i++)
        {
            Debug.Log(saves[i]);
        }
    });
    */
    [UnityTest]
    public IEnumerator TestCreateSaveData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();
        
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "save_data.json");
        
        var dataStore = new JsonAsyncDataStore<SaveData[]>(path);
        
        await dataStore.StoreAsync(new []
        {
            new SaveData()
            {
                Id = 0,
                IsAutoSave = false,
                SavedLocationName = "Hallo World",
                SavedAt = DateTimeOffset.Now
            },
            new SaveData()
            {
                Id = 1,
                IsAutoSave = true,
                SavedLocationName = "Hallo World1",
                SavedAt = DateTimeOffset.Now
            }
        }, cts.Token);
    });

    [UnityTest]
    public IEnumerator TestLoadSaveData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();

        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "save_data.json");

        var dataStore = new JsonAsyncDataStore<SaveData[]>(path);

        var saves = await dataStore.LoadAsync(cts.Token);
        for (var i = 0; i < saves?.Length; i++)
        {
            Debug.Log(saves[i]);
        }
    });

    [UnityTest]
    public IEnumerator TestFindSaveDataUscCase() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();

        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "save_data.json");
        
        IAsyncDataStore<SaveData[]> dataStore = new JsonAsyncDataStore<SaveData[]>(path);
        
        IAsyncRepository<SaveData,SaveDataId> repository=new SaveDataRepository(dataStore);

        var useCase = new FindSaveDataUseCase(repository);
        var saves = await useCase.FindAllAsync(cts.Token);
        foreach (var save in saves)
        {
            Debug.Log(save);
        }
    });
}