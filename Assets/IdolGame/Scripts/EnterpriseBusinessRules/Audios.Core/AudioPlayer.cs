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

    public async UniTask PlayBgmAsync(AssetReference? assetReference, CancellationToken ct)
    {
        await service.PlayBgmAsync(assetReference, 1.0f, ct);
    }

    public async UniTask PlaySeAsync(AssetReference? assetReference, CancellationToken ct)
    {
        await service.PlaySeAsync(assetReference, 1.0f, ct);
    }

    public async UniTask StopBgmAsync(AssetReference? assetReference, CancellationToken ct)
    {
        await service.StopBgmAsync(assetReference, 1.0f, ct);
    }
}