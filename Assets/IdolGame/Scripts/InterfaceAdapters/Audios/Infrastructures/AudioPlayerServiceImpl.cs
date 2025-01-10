using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.Audios.Core;
using Microsoft.Extensions.Logging;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace IdolGame.Audios.Infrastructures;

[Serializable]
public struct AudioPlayerSettings
{
    public int maxBgmSources;// 最大BGMソース数
    public int maxSeSources;// 最大SEソース数
}

public sealed class AudioPlayerServiceImpl:IAudioPlayerService
{
    readonly ILogger<AudioPlayerServiceImpl> logger;
    readonly AudioPlayerSettings settings; // オーディオプレイヤーの設定
    readonly GameObject gameObjcet; // オーディオを再生するゲームオブジェクト
    readonly IAudioLoader loader;// オーディオローダーのインターフェース
    List<AudioSource>? bgmSources;
    List<AudioSource>? seSources; 
    bool initialized;
    
    
    public AudioPlayerServiceImpl(ILogger<AudioPlayerServiceImpl> logger, AudioPlayerSettings settings, GameObject gameObjcet, IAudioLoader loader)
    {
        this.logger = logger;
        this.settings = settings;
        this.gameObjcet = gameObjcet;
        this.loader = loader;
    }

   
    public async UniTask InitializeAsync(CancellationToken ct)
    {
        if (initialized)
        {
            return;
        }
        
        initialized = true;

        bgmSources = new List<AudioSource>(capacity: settings.maxBgmSources);
        for (var i = 0; i < settings.maxBgmSources; i++)
        {
            var source = gameObjcet.AddComponent<AudioSource>();
            source.playOnAwake = false;
            bgmSources.Add(source);
        }
        
        await UniTask.Yield(ct);
        
        seSources = new List<AudioSource>(capacity: settings.maxSeSources);
        for ( var i = 0; i < settings.maxSeSources; i++)
        {
            var source = gameObjcet.AddComponent<AudioSource>();
            source.playOnAwake = false;
            seSources.Add(source);
        }
        
        await UniTask.Yield(ct);
    }

    // BGMを再生する非同期メソッド
    public async UniTask PlayBgmAsync(AssetReference? reference, float volume, CancellationToken ct)
    {
        if (!initialized)
        {
            logger.LogWarning($"{GetType().Name}has not initialized");
            return;
        }

        var clip = await loader.LoadAsync(reference, ct);
        // var clip =  await loader.LoadAsync(reference, ct);
        if (clip is null)
        {
            logger.LogWarning($"{GetType().Name}has not loaded");
            return;
        }

        var emptySource = bgmSources?.Find(static x => !x.isPlaying);
        if (emptySource is null)
        {
            logger.LogWarning($"{GetType().Name}has not emptySource");
            return;
        }
        
        emptySource.clip = clip;
        emptySource.volume = volume;
        emptySource.Play();
    }

    // SEを再生する非同期メソッド
    public async UniTask PlaySeAsync(AssetReference? reference, float volume, CancellationToken ct)
    {
        if (!initialized)
        {
            logger.LogWarning($"{GetType().Name}has not initialized");
            return;
        }

        var clip = await loader.LoadAsync(reference, ct);
        // var clip =  await loader.LoadAsync(reference, ct);
        if (clip is null)
        {
            logger.LogWarning($"{GetType().Name}has not loaded");
            return;
        }

        var emptySource = seSources?.Find(static x => !x.isPlaying);
        if (emptySource is null)
        {
            logger.LogWarning($"{GetType().Name}has not emptySource");
            return;
        }
        
        emptySource.volume = volume;
        emptySource.PlayOneShot(clip);
    }

    // BGMを停止する非同期メソッド
    public async UniTask StopBgmAsync(AssetReference? reference, float duration, CancellationToken ct)
    {
        if (!initialized)
        {
            logger.LogWarning($"{GetType().Name}has not initialized");
            return;
        }

        var clip = await loader.LoadAsync(reference, ct);
        // var clip =  await loader.LoadAsync(reference, ct);
        if (clip is null)
        {
            logger.LogWarning($"{GetType().Name}has not loaded");
            return;
        }

        var playedSource = bgmSources?.Find( x => x.clip == clip);
        if (playedSource is null)
        {
            logger.LogWarning($"{GetType().Name}has not playedSource");
            return;
        }
        playedSource.Stop();
    }
}