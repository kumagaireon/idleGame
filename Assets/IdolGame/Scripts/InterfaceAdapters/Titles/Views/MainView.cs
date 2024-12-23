using IdolGame.Common.Views;
using UnityEngine.UIElements;

namespace IdolGame.Titles.Views;

/// <summary>
/// タイトル画面のメインビュークラス
/// </summary>
public sealed class MainView: ViewBase
{  
    // アプリのバージョン情報を表示するテキスト要素

    public VisualElement TouchPanel { get;}

    public VisualElement IdolTextElement { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="asset">VisualTreeAssetからビューを構築するためのアセット</param>
    public MainView(VisualTreeAsset asset) : base(asset)
    {
        // ビューから指定された名前のテキスト要素を取得

        TouchPanel = OwnView.Q<VisualElement>("touch-panel");
       // IdolTextElement.style.backgroundImage = Background.FromTexture2D(result);

    }
}