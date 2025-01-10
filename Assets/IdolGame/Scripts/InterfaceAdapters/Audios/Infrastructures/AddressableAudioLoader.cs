using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace IdolGame.Audios.Infrastructures;

// AddressableAudioLoaderクラスは、オーディオクリップの読み込みとアンロードを管理するクラス
public sealed class AddressableAudioLoader:IAudioLoader,IDisposable
{
    // AudioClipDisposableクラスは、オーディオクリップとそのリリース処理を管理
    class AudioClipDisposable
    {
        readonly IDisposable? disposable;
        
        public AudioClip Clip { get; }

        public AudioClipDisposable(AudioClip? clip,AssetReference? reference)
        {
            Clip = clip;
            disposable = Disposable.Create(reference, static x => x?.ReleaseAsset());
        }
        
        // Disposeメソッドでリソースを解放
        public void Dispose()=>disposable?.Dispose();
    }
    
    // キャッシュとして使用する辞書。キャッシュの容量は24。
    private readonly Dictionary<string, AudioClipDisposable?> cache = new(capacity: 24);
    
    // AssetReferenceを使用してオーディオクリップを非同期で読み込み
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
            // クリップがロードされるまで待機
            await UniTask.WaitUntil(clip, static x => x is not null, cancellationToken: ct);
        }
        else
        {
            // キャッシュに追加
            cache.Add(reference.AssetGUID,null);
            clip = await reference.LoadAssetAsync<AudioClip>().WithCancellation(ct);
            
            // オーディオデータをロード
            clip.LoadAudioData();
            await UniTask.WaitUntil(clip, static x => x.loadState == AudioDataLoadState.Loaded, cancellationToken: ct);

            // キャッシュにオーディオクリップを追加
            cache[reference.AssetGUID] = new AudioClipDisposable(clip, reference);
        }

        return clip;
    }

    // AssetReferenceを使用してオーディオクリップを非同期でアンロード
    public async UniTask UnLoadAsync(AssetReference reference, CancellationToken ct)
    {
        if (reference is null
            || !cache.TryGetValue(reference.AssetGUID, out var value)
            || value == null)
        {
            return;
        }
        
        // オーディオデータをアンロードし、リソースを解放
        value.Clip?.UnloadAudioData();
        value.Dispose();
        
        // キャッシュから削除
        cache.Remove(reference.AssetGUID);
        await UniTask.Yield(ct);
    }

    // Disposeメソッドでキャッシュ内のすべてのリソースを解放
    public void Dispose()
    {
        foreach (var value in cache.Values)
        {
            value?.Dispose();
        }
        cache.Clear();
    }
}