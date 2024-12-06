using IdolGame.Common.Views;
using UnityEngine.UIElements;

namespace IdolGame.Scripts.InterfaceAdapters.Gallery.Views;

public sealed class MainView: ViewBase
{  
    // アプリのバージョン情報を表示するテキスト要素
    public TextElement AppInfoVersionTextElement { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="asset">/// <param name="asset">VisualTreeAssetからビューを構築するためのアセット</param>
    public MainView(VisualTreeAsset asset) : base(asset)
    { 
        // ビューから指定された名前のテキスト要素を取得
        AppInfoVersionTextElement = OwnView.Q<TextElement>("app-info-version-text");
    }
}