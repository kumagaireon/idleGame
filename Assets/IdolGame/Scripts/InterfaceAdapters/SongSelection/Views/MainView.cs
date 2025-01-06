using IdolGame.Common.Views;
using IdolGame.UIElements;
using UnityEngine.UIElements;

namespace IdolGame.SongSelection.Views;

public sealed class MainView : ViewBase
{
    public VisualElement MusicJacketVisualElement { get; }
    public TextElement ExplanationSongSelectionTextElement { get; }
    public VisualElement StartVisualElement { get; }
    public VisualElement ReturnVisualElement { get; }

    public CustomScrollView SongSelectionScrollView { get; }
    public MainView(VisualTreeAsset asset) : base(asset)
    {
        MusicJacketVisualElement = OwnView.Q<VisualElement>("music-jacket");
        ExplanationSongSelectionTextElement = OwnView.Q<TextElement>("explanation-song-selection");
        StartVisualElement = OwnView.Q<VisualElement>("start-button-pos");
        ReturnVisualElement = OwnView.Q<VisualElement>("back-button");
        SongSelectionScrollView= OwnView.Q<CustomScrollView>("song-selection-data-list-view");
    }
}