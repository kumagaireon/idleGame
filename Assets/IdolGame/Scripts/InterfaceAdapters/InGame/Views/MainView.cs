using IdolGame.Common.Views;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace IdolGame.InGame.Views;

public sealed class MainView: ViewBase
{  
    public RawImage video { get; }
    
    public MainView(VisualTreeAsset asset) : base(asset)
    { 
        
    }
}