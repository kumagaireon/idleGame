using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace IdolGame.Audios.Core;

public interface IAudioPlayerService
{
     UniTask InitializeAsync(CancellationToken ct);
     UniTask PlayBgmAsync(AssetReference? reference,float volume, CancellationToken ct);
     UniTask PlaySeAsync(AssetReference? reference,float volume, CancellationToken ct);
     UniTask StopBgmAsync(AssetReference? reference,float duration, CancellationToken ct);
}