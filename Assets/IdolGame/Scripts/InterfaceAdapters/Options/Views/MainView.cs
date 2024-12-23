using IdolGame.Common.Views;
using IdolGame.UIElements;
using UnityEngine.UIElements;

namespace IdolGame.Options.Views;

public sealed class MainView: ViewBase
{  
    // アプリのバージョン情報を表示するテキスト要素
    public OptionsDropdownUIElement GraphicsSettingsOptions { get; }
    public OptionsButtonUIElement SoundEnabledOptions { get; }
    public OptionsSliderUIElement BgmVolumeOptions { get; }
    public OptionsSliderUIElement SeVolumeOptions { get; }


    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="asset">/// <param name="asset">VisualTreeAssetからビューを構築するためのアセット</param>
    public MainView(VisualTreeAsset asset) : base(asset)
    { 
        GraphicsSettingsOptions = OwnView.Q<OptionsDropdownUIElement>("quality-settings-options");
        SoundEnabledOptions = OwnView.Q<OptionsButtonUIElement>("sound-enabled-options");
        BgmVolumeOptions = OwnView.Q<OptionsSliderUIElement>("bgm-volume-options");
        SeVolumeOptions = OwnView.Q<OptionsSliderUIElement>("se-volume-options");
        
    }
}