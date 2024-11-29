using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace IdolGame.Audios.Infrastructures;

public interface IAudioLoader
{
    UniTask<AudioClip?> LoadAsync(AssetReference reference, CancellationToken ct);
    
    UniTask UnLoadAsync(AssetReference reference, CancellationToken ct);
}