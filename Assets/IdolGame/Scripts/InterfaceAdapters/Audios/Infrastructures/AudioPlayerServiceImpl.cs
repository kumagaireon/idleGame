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
    public int maxBgmSources;
    public int maxSeSources;
}

public sealed class AudioPlayerServiceImpl:IAudioPlayerService
{
    readonly ILogger<AudioPlayerServiceImpl> logger;
    readonly AudioPlayerSettings settings; 
    readonly GameObject gameObjcet; 
    readonly IAudioLoader loader;
    List<AudioSource>? bgmSources;
    List<AudioSource>? seSources; 
    bool initialized;
    
    
    public AudioPlayerServiceImpl(ILogger<AudioPlayerServiceImpl> logger, AudioPlayerSettings settings, GameObject gameObjcet)
    {
        this.logger = logger;
        this.settings = settings;
        this.gameObjcet = gameObjcet;
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