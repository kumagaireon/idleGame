using IdolGame.Common.Views;
using UnityEngine.UIElements;

namespace IdolGame.Results.Views;

public sealed class MainView: ViewBase
{  
    // アプリのバージョン情報を表示するテキスト要素
    public TextElement IdolSupportDialogueTextElement { get; }
    public TextElement ResultPointsTextElement { get; }
    public VisualElement IdolImageVisualElement { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="asset">/// <param name="asset">VisualTreeAssetからビューを構築するためのアセット</param>
    public MainView(VisualTreeAsset asset) : base(asset)
    { 
        // ビューから指定された名前のテキスト要素を取得
        IdolSupportDialogueTextElement = OwnView.Q<TextElement>("idol-support-dialogue-text");
        ResultPointsTextElement = OwnView.Q<TextElement>("result-points-text");
        IdolImageVisualElement = OwnView.Q<VisualElement>("idol-image");
    }
}