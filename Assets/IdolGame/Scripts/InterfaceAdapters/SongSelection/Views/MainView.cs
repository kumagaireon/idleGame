using IdolGame.Common.Views;
using UnityEngine.UIElements;

namespace IdolGame.SongSelection.Views;

public sealed class MainView: ViewBase
{  
    // アプリのバージョン情報を表示するテキスト要素
    public TextElement AppInfoVersionTextElement { get; }

    public MainView(VisualTreeAsset asset) : base(asset)
    { 
        AppInfoVersionTextElement = OwnView.Q<TextElement>("app-info-version-text");
    }
}