using System;
using System.Collections;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;
using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.EnterpriseBusinessRules;
using IdolGame.Frameworks;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public sealed class DataUseCaseTester
{
    //===========確認用===============
    [UnityTest]
    public IEnumerator TestCreateKakuninData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();

        // データストアのパスを設定
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "kakunin_data.json");

        // データストアを初期化
        var dataStore = new JsonAsyncDataStore<ResultIdolData[]>(path);

        // サンプルのアイドルグループデータを作成し、データストアに保存
        await dataStore.StoreAsync(new[]
        {
            new ResultIdolData
            {
                SRank = "Sランク用テキスト",
                ARank = "Aランク用テキスト",
                BRank = "Bランク用テキスト",
                CRank = "Cランク用テキスト",
                SRankPoint = 2000.750f,
                ARankPoint = 1500.500f,
                BRankPoint = 1000.250f,
                CRankPoint = 500.00f,
                SRankVoice = "Sランク用ボイスパス",
                ARankVoice = "Aランク用ボイスパス",
                BRankVoice = "Bランク用ボイスパス",
                CRankVoice = "Cランク用ボイスパス"
            }
        }, cts.Token);
    });
    
    //===========選曲Data===============
    [UnityTest]
    public IEnumerator TestCreateMusicSelectData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "music_data.json");
        var dataStore = new JsonAsyncDataStore<MusicData[]>(path);
        /*await dataStore.StoreAsync(new[]
        {
            new MusicData()
            {
                Id = 1,
                Name = "ここに曲名1",
                ImagePath = "ここに画像のパス2",
                Description = "ここに曲の説明文を書いて3",
                VoicePath = "ここに動画パス書いて４"
            }
        }, cts.Token);*/
    });

    
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

    //===========選曲Data===============
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

    //===========推しアイドル===============

    [UnityTest]
    public IEnumerator TestCreateFavoriteIdolGroupData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();

        // データストアのパスを設定
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "favorite_idol_data.json");

        // データストアを初期化
        var dataStore = new JsonAsyncDataStore<IdolGroupData[]>(path);
      
        // サンプルのアイドルグループデータを作成し、データストアに保存
        await dataStore.StoreAsync(new[]
        {
            new IdolGroupData
            {
                GroupId = 1,
                GroupName = "Super Idols Group",
                GroupImagelogoPath = "path/to/logo1.png",
                ImagePath="path/to/logo2.png",
                Members =null
            }
        }, cts.Token);
    });

    [UnityTest]
    public IEnumerator TestCreateIdolData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();

        // データストアのパスを設定
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "kakunin_data.json");

        // データストアを初期化
        var dataStore = new JsonAsyncDataStore<IdolMembersData[]>(path);

        // サンプルのアイドルグループデータを作成し、データストアに保存
        await dataStore.StoreAsync(new[]
        {
            new IdolMembersData
            {
                Id = 1,
                Name = "アイドル",
                ImagePath = "path/to/logo2.png",
                CollarCode="FF0000"
            }
        }, cts.Token);
    });

    [UnityTest]
    public IEnumerator TestLoadFavoriteIdolData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();

        var path = Path.Combine(Application.streamingAssetsPath,  "master_data", "favorite_idol_data.json");

        var dataStore = new JsonAsyncDataStore<IdolGroupData[]>(path);

        var idolGroup = await dataStore.LoadAsync(cts.Token);
        for (var i = 0; i < idolGroup?.Length; i++)
        {
            Debug.Log(idolGroup[i]);
        }
    });
    //===========推しアイドル===============
    

    //===========セーブデータ===============
    
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
    
    //===========セーブデータ===============
}