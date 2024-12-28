using System;
using System.Collections;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame;
using IdolGame.ApplicationBusinessRules.Interfaces;
using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Frameworks;
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
        var dataStore = new JsonAsyncDataStore<IdolRewardData[]>(path);

        // サンプルのアイドルグループデータを作成し、データストアに保存
        await dataStore.StoreAsync(new[]
        {
            new IdolRewardData
            {
                RewardChekiImage1Path="aa",
                RewardChekiImage2Path="bb",
                RewardChekiImage3Path="cc",
                RewardVicePath="ss",
                DateAcquisitioRewardCheck1= DateTimeOffset.Now,
                DateAcquisitioRewardCheck2= DateTimeOffset.Now,
                DateAcquisitioRewardCheck3 =  DateTimeOffset.Now,
                DateAcquisitioRewardCheck= DateTimeOffset.Now,
                IdolPoint = (IdolRewardPoint)0.0
            }
        }, cts.Token);
    });
   
    [UnityTest]
    public IEnumerator TestLoadKakuninData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();
        // JSONデータファイルのパスを構築
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "kakunin_data.json");

        var dataStore = new JsonAsyncDataStore<IdolGroupData[]>(path);
        // データストアからセーブデータを非同期にロード
        var saves = await dataStore.LoadAsync(cts.Token);
        // ロードしたデータをデバッグログに出力
        for (var i = 0; i < saves.Length; i++)
        {
            Debug.Log(saves[i]);
        }
    });
  
    
    //===========選曲Data===============
    [UnityTest]
    public IEnumerator TestCreateMusicData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();

        // データストアのパスを設定
        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "music_data.json");
        
        // データストアを初期化
        var dataStore = new JsonAsyncDataStore<MusicData[]>(path);
        await dataStore.StoreAsync(new[]
        {
            new MusicData
            {
                Id = 1,
                Name = "曲名",
                ImagePath = "画像パス",
                Description= "説明文",
                VoicePath = "動画パス"
            }
        }, cts.Token);
    });
    [UnityTest]
    public IEnumerator TestLoadMusicData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();

        var path = Path.Combine(Application.streamingAssetsPath,  "master_data", "music_data.json");

        var dataStore = new JsonAsyncDataStore<MusicData[]>(path);

        var idolGroup = await dataStore.LoadAsync(cts.Token);
        for (var i = 0; i < idolGroup?.Length; i++)
        {
            Debug.Log(idolGroup[i]);
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