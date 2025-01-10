using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.Audios.Core;
using Microsoft.Extensions.Logging;
using UnityEngine.AddressableAssets;

namespace IdolGame.Audios.Infrastructures;

// AddressableAudioPreloaderクラスは、オーディオクリップの事前読み込みを管理するクラス
public sealed class AddressableAudioPreloader : IAudioPreloader
{
    readonly ILogger<AddressableAudioPreloader> logger;

    // オーディオローダーのインターフェース
    readonly IAudioLoader loader;

    // 事前読み込みするアセット参照の配列
    readonly AssetReference?[] preloadReferences;

    public AddressableAudioPreloader(ILogger<AddressableAudioPreloader> logger,
        IAudioLoader loader,
        AssetReference?[] preloadReferences)
    {
        this.logger = logger;
        this.loader = loader;
        this.preloadReferences = preloadReferences;
    }

    // オーディオクリップを事前に読み込む非同期メソッド
    public async UniTask PreloadAsync(CancellationToken ct)
    {
        // 各アセット参照について、ローダーを使用して非同期で読み込む
        foreach (var reference in preloadReferences)
        {
            await loader.LoadAsync(reference, ct);
        }
    }
}