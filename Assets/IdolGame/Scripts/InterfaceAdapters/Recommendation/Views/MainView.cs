using IdolGame.Common.Views;
using IdolGame.UIElements;
using UnityEngine.UIElements;

namespace IdolGame.Recommendation.Views;

public sealed class MainView : ViewBase
{
    public VisualElement IdolGroupLogoImageElement { get; }
    public TextElement ExplanatoryTextElementext { get; }
    public VisualElement GalleryVisualElement { get; }
    public VisualElement ReturnVisualElement { get; }
    public CustomScrollView IdolScrollView { get; }

    public MainView(VisualTreeAsset asset) : base(asset)
    {
        IdolGroupLogoImageElement = OwnView.Q<VisualElement>("idol-logo");
        ExplanatoryTextElementext = OwnView.Q<TextElement>("explanatory-text");
        GalleryVisualElement = OwnView.Q<VisualElement>("gallery-button");
        ReturnVisualElement = OwnView.Q<VisualElement>("back-button");
        IdolScrollView = OwnView.Q<CustomScrollView>("idol-scroll-view");
    }
}