using IdolGame.Common.Views;
using UnityEngine.UIElements;

namespace IdolGame.Menu.Views;

public sealed class MainView: ViewBase
{  
    // アプリのバージョン情報を表示するテキスト要素
    public TextElement AppInfoVersionTextElement { get; }
 
    public TextElement SongSelectionTextElement { get; } 
    public TextElement AboutMeTextElement { get; } 
    public TextElement IdolSpeechBubbleTextElement { get; } 
    
    public VisualElement IdolTextElement { get; }
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="asset">/// <param name="asset">VisualTreeAssetからビューを構築するためのアセット</param>
    public MainView(VisualTreeAsset asset) : base(asset)
    { 
        // ビューから指定された名前のテキスト要素を取得
        AppInfoVersionTextElement = OwnView.Q<TextElement>("app-info-version-text");
        
        SongSelectionTextElement= OwnView.Q<TextElement>("song-selection-button");
        AboutMeTextElement= OwnView.Q<TextElement>("about-me-button");
        IdolSpeechBubbleTextElement= OwnView.Q<TextElement>("idol-speech-bubble-text");
         
    }
}