using System.Threading;
using Cysharp.Threading.Tasks;

namespace IdolGame.Audios.Core;

public interface IAudioPreloader
{
    UniTask PreloadAsync(CancellationToken ct);
}