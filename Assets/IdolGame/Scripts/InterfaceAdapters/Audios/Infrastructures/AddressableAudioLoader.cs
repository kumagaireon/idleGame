using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace IdolGame.Audios.Infrastructures;

public sealed class AddressableAudioLoader:IAudioLoader,IDisposable
{
    class AudioClipDisposable
    {
        readonly IDisposable? disposable;
        
        public AudioClip Clip { get; }

        public AudioClipDisposable(AudioClip? clip,AssetReference? reference)
        {
            Clip = clip;
            disposable = Disposable.Create(reference, static x => x?.ReleaseAsset());
        }
        
        public void Dispose()=>disposable?.Dispose();
    }
    
    private readonly Dictionary<string, AudioClipDisposable?> cache = new(capacity: 24);
    
    public async UniTask<AudioClip?> LoadAsync(AssetReference reference, CancellationToken ct)
    {
        if (reference is null)
        {
            return null;
        }

        AudioClip? clip;
        if (cache.TryGetValue(reference.AssetGUID, out var value))
        {
            clip = value.Clip;
            await UniTask.WaitUntil(clip, static x => x is not null, cancellationToken: ct);
        }
        else
        {
            cache.Add(reference.AssetGUID,null);
            clip = await reference.LoadAssetAsync<AudioClip>().WithCancellation(ct);
            
            clip.LoadAudioData();
            await UniTask.WaitUntil(clip, static x => x.loadState == AudioDataLoadState.Loaded, cancellationToken: ct);

            cache[reference.AssetGUID] = new AudioClipDisposable(clip, reference);
        }

        return clip;
    }

    public async UniTask UnLoadAsync(AssetReference reference, CancellationToken ct)
    {
        if (reference is null
            || !cache.TryGetValue(reference.AssetGUID, out var value)
            || value == null)
        {
            return;
        }
        
        value.Clip?.UnloadAudioData();
        value.Dispose();
        
        cache.Remove(reference.AssetGUID);
        await UniTask.Yield(ct);
    }

    public void Dispose()
    {
        foreach (var value in cache.Values)
        {
            value?.Dispose();
        }
        cache.Clear();
    }
}