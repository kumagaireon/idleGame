using IdolGame.Common.Views;
using UnityEngine.UIElements;

namespace IdolGame.Results.Views;

public sealed class MainView: ViewBase
{  
    public VisualElement BackgroundImageVisualElement { get; }
   
    public VisualElement IdolImageVisualElement { get; }
    public TextElement IdolSupportDialogueTextElement { get; }
    
    public VisualElement ResultPointImageVisualElement { get; }
    public TextElement ResultPointsTextElement { get; }
    
    public VisualElement  MenuVisualElement { get; }
    public VisualElement  RetryVisualElement { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="asset">/// <param name="asset">VisualTreeAssetからビューを構築するためのアセット</param>
    public MainView(VisualTreeAsset asset) : base(asset)
    { 
        BackgroundImageVisualElement=OwnView.Query<VisualElement>("background");
        
        IdolImageVisualElement = OwnView.Q<VisualElement>("idol-image");
        IdolSupportDialogueTextElement = OwnView.Q<TextElement>("idol-support-dialogue-text");
        
        ResultPointImageVisualElement = OwnView.Q<VisualElement>("result-point-image");
        ResultPointsTextElement = OwnView.Q<TextElement>("result-points-text");
      
        MenuVisualElement = OwnView.Q<VisualElement>("menu-button");
        RetryVisualElement = OwnView.Q<VisualElement>("retry-button");
    }
}