using UnityEngine.AddressableAssets;

namespace IdolGame.Audios.Core;

public interface IAudioTable
{
    AssetReference? TitleBgmReference { get; }
}