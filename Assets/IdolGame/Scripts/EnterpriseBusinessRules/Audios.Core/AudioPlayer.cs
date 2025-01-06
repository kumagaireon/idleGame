using System.Threading;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnityEngine.AddressableAssets;

namespace IdolGame.Audios.Core;

public sealed class AudioPlayer
{
    readonly ILogger<AudioPlayer> logger;
    readonly IAudioPlayerService service;

    public AudioPlayer(ILogger<AudioPlayer> logger, IAudioPlayerService service)
    {
        this.logger = logger;
        this.service = service;
    }

    public async UniTask InitializeAsync(CancellationToken ct)
    {
        await service.InitializeAsync(ct);
    }

    public async UniTask PlayBgmAsync(AssetReference? reference, CancellationToken ct)
    {
        await service.PlayBgmAsync(reference, 1.0f, ct);
    }
    
    public async UniTask PlayBgmAsync(AssetReference? reference, float volume, CancellationToken ct)
    {
        await service.PlayBgmAsync(reference, volume, ct);
    }
    
    public async UniTask PlaySeAsync(AssetReference? reference, CancellationToken ct)
    {
        await service.PlaySeAsync(reference, 1.0f, ct);
    }

    public async UniTask PlaySeAsync(AssetReference? reference, float volume, CancellationToken ct)
    {
        await service.PlaySeAsync(reference, volume, ct);
    }

    public async UniTask StopBgmAsync(AssetReference? reference, float duration, CancellationToken ct)
    {
        await service.StopBgmAsync(reference, duration, ct);
    }
    
    public async UniTask StopBgmAsync(AssetReference? reference, CancellationToken ct)
    {
        await service.StopBgmAsync(reference, 0.0f, ct);
    }
}