using System.Threading;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UnityEngine.AddressableAssets;

namespace IdolGame.Audios.Core;

public sealed class AudioPlayer
{
    readonly ILogger<AudioPlayer> logger;
    // オーディオプレイヤーサービスのインターフェース
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

    // BGMを再生する非同期メソッド（デフォルトボリューム1.0）
    public async UniTask PlayBgmAsync(AssetReference? reference, CancellationToken ct)
    {
        await service.PlayBgmAsync(reference, 1.0f, ct);
    }
    
    // BGMを再生する非同期メソッド（指定ボリューム）
    public async UniTask PlayBgmAsync(AssetReference? reference, float volume, CancellationToken ct)
    {
        await service.PlayBgmAsync(reference, volume, ct);
    }
    
    // SEを再生する非同期メソッド（デフォルトボリューム1.0）
    public async UniTask PlaySeAsync(AssetReference? reference, CancellationToken ct)
    {
        await service.PlaySeAsync(reference, 1.0f, ct);
    }

    // SEを再生する非同期メソッド（指定ボリューム）
    public async UniTask PlaySeAsync(AssetReference? reference, float volume, CancellationToken ct)
    {
        await service.PlaySeAsync(reference, volume, ct);
    }

    // BGMを停止する非同期メソッド（指定持続時間）
    public async UniTask StopBgmAsync(AssetReference? reference, float duration, CancellationToken ct)
    {
        await service.StopBgmAsync(reference, duration, ct);
    }
    
    // BGMを即時停止する非同期メソッド
    public async UniTask StopBgmAsync(AssetReference? reference, CancellationToken ct)
    {
        await service.StopBgmAsync(reference, 0.0f, ct);
    }
}