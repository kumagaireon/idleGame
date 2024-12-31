using IdolGame.Common.Views;
using IdolGame.UIElements;
using UnityEngine.UIElements;

namespace IdolGame.Options.Views;

public sealed class OptionsView : ViewBase
{
    public OptionsDropdownUIElement GraphicsSettingsOptions { get; }
    public OptionsButtonUIElement SoundEnabledOptions { get; }
    public OptionsSliderUIElement BgmVolumeOptions { get; }
    public OptionsSliderUIElement SeVolumeOptions { get; }

    public OptionsView(VisualTreeAsset asset) : base(asset)
    {
        GraphicsSettingsOptions = OwnView.Q<OptionsDropdownUIElement>("quality-settings-options");
        SoundEnabledOptions = OwnView.Q<OptionsButtonUIElement>("sound-enabled-options");
        SeVolumeOptions = OwnView.Q<OptionsSliderUIElement>("se-volume-options");
    }
}