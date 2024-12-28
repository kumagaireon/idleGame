using IdolGame.Common.Views;
using UnityEngine.UIElements;

namespace IdolGame.Menu.Views;

public sealed class MainView : ViewBase
{
    public VisualElement OptionVisualElement { get; }
    public VisualElement SongSelectionVisualElement { get; }
    public VisualElement AboutMeVisualElement { get; }
    public VisualElement BackgroundImageVisualElement { get; }
    public VisualElement IdolImageVisualElement { get; }
    public TextElement IdolSpeechBubbleTextElement { get; }



    public MainView(VisualTreeAsset asset) : base(asset)
    {
        OptionVisualElement = OwnView.Q<VisualElement>("option-button");
        SongSelectionVisualElement = OwnView.Q<VisualElement>("song-selection-button");
        AboutMeVisualElement = OwnView.Q<VisualElement>("about-me-button");
        BackgroundImageVisualElement= OwnView.Q<VisualElement>("background");
        IdolImageVisualElement = OwnView.Q<VisualElement>("idol-image");
        IdolSpeechBubbleTextElement = OwnView.Q<TextElement>("idol-speech-bubble-text");
    }
}