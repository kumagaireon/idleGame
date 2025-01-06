using IdolGame.Common.Views;
using IdolGame.UIElements;
using UnityEngine.UIElements;

namespace IdolGame.Options.Views;

public sealed class MainView: ViewBase
{  
    public OptionsSliderUIElement BgmVolumeOptions { get; }


  
    public MainView(VisualTreeAsset asset) : base(asset)
    { 
        BgmVolumeOptions = OwnView.Q<OptionsSliderUIElement>("bgm-volume-options");
    }
}