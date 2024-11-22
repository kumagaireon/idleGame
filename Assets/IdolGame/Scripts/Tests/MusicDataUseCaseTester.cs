


using System.Collections;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;
using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.EnterpriseBusinessRules;
using IdolGame.Frameworks;
using IdolGame.InterfaceAdapters.Frameworks;
using UnityEngine;
using UnityEngine.TestTools;

public sealed class MusicDataUseCaseTester
{
    /*
    [UnityTest]
    public IEnumerator TestCreateSaveData() => UniTask.ToCoroutine(async () =>
    {

        var cts = new CancellationTokenSource();

        var path = Path.Combine(Application.persistentDataPath, "master_data", "music_data.json");

        var dataStore = new JsonAsyncDataStore<MusicData[]>(path);
        
        await dataStore.StoreAsync(new []
        {
            new MusicData()
            {
                new MusicId(1),
                new MusicName("mama"), 
                new MusicImagePath("iei"),
                new MusicDescription("baka")
            }
        }, cts.Token);
    });
    */
    
    [UnityTest]
    public IEnumerator TestLoadSaveData() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();

        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "music_data.json");

        var dataStore = new JsonAsyncDataStore<MusicData[]>(path);
        
        var saves = await dataStore.LoadAsync(cts.Token);

        for (var i = 0; i < saves.Length; i++)
        {
            Debug.Log(saves[i]);
        }
    });
    [UnityTest]
    public IEnumerator TestFindSaveDataUscCase() => UniTask.ToCoroutine(async () =>
    {
        var cts = new CancellationTokenSource();

        var path = Path.Combine(Application.streamingAssetsPath, "master_data", "music_data.json");

        IAsyncDataStore<MusicData[]> dataStore = new JsonAsyncDataStore<MusicData[]>(path);

        IAsyncRepository<MusicData, MusicId> repository = new MusicDataRepository(dataStore);
        var useCase = new FindMusicDataUseCase(repository);
        var saves = await useCase.FindAllAsync(cts.Token);
        foreach (var save in saves)
        {
            Debug.Log(save);
        }
    });
}