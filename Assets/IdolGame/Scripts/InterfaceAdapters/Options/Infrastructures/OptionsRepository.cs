using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace IdolGame.Options.Infrastructures;

public class OptionsRepository
{
    public async UniTask SaveOptionsAsync(OptionsData optionsData, CancellationToken ct)
    {
        // 実際の保存処理 (例: PlayerPrefs やファイル保存)
        PlayerPrefs.SetInt("GraphicsQuality", optionsData.GraphicsQuality);
        PlayerPrefs.SetInt("SoundEnabled", optionsData.SoundEnabled ? 1 : 0);
        PlayerPrefs.SetFloat("BgmVolume", optionsData.BgmVolume);
        PlayerPrefs.SetFloat("SeVolume", optionsData.SeVolume);
        PlayerPrefs.Save();
        await UniTask.Yield(ct);
    }

    public async UniTask<OptionsData> LoadOptionsAsync(CancellationToken ct)
    {
        var optionsData = new OptionsData
        {
            GraphicsQuality = PlayerPrefs.GetInt("GraphicsQuality", 2),
            SoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1,
            BgmVolume = PlayerPrefs.GetFloat("BgmVolume", 0.5f), SeVolume = PlayerPrefs.GetFloat("SeVolume", 0.5f)
        };
        await UniTask.Yield(ct);
        return optionsData;
    }

}

public class OptionsData
{
    public int GraphicsQuality { get; set; }
    public bool SoundEnabled { get; set; }
    public float BgmVolume { get; set; }
    public float SeVolume { get; set; }
}