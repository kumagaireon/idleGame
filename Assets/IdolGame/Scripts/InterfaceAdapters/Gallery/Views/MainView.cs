using IdolGame.Common.Views;
using UnityEngine.UIElements;

namespace IdolGame.Scripts.InterfaceAdapters.Gallery.Views;

public sealed class MainView : ViewBase
{
    public VisualElement ReturnVisualElement { get; }
    
    public VisualElement IdolPic1VisualElementElement { get; }
    public VisualElement IdolPic2VisualElementElement { get; }
    public VisualElement IdolPic3VisualElementElement { get; }
    public VisualElement IdolVideoVisualElementElement { get; }

    //解放した日付
    public TextElement IdolChekiDate1TextElement { get; }
    public TextElement IdolChekiDate2TextElement { get; }
    public TextElement IdolChekiDate3TextElement { get; }
    public TextElement IdolVideoDateTextElement { get; }

    //解放まで残り○○ポイント的なのは直書き○○は必要ポイント
    public TextElement IdleChekiReleasePoint1TextElement { get; }
    public TextElement IdleChekiReleasePoint2TextElement { get; }
    public TextElement IdleChekiReleasePoint3TextElement { get; }
    public TextElement IdolVideoReleasePointTextElement { get; }
    public TextElement AccumulatedPointTextElement { get; }


    public MainView(VisualTreeAsset asset) : base(asset)
    {
        // ビューから指定された名前のテキスト要素を取得
        IdolPic1VisualElementElement = OwnView.Q<VisualElement>("idol-pic-1");
        IdolPic2VisualElementElement = OwnView.Q<VisualElement>("idol-pic-2");
        IdolPic3VisualElementElement = OwnView.Q<VisualElement>("idol-pic-3");
        IdolVideoVisualElementElement = OwnView.Q<VisualElement>("idol-video");

        IdolChekiDate1TextElement = OwnView.Q<TextElement>("idle-cheki-date-1");
        IdolChekiDate2TextElement = OwnView.Q<TextElement>("idle-cheki-date-2");
        IdolChekiDate3TextElement = OwnView.Q<TextElement>("idle-cheki-date-3");
        IdolVideoDateTextElement = OwnView.Q<TextElement>("idol-video-date");

        IdleChekiReleasePoint1TextElement = OwnView.Q<TextElement>("idle-cheki-release-point-1");
        IdleChekiReleasePoint2TextElement = OwnView.Q<TextElement>("idle-cheki-release-point-2");
        IdleChekiReleasePoint3TextElement = OwnView.Q<TextElement>("idle-cheki-release-point-3");
        IdolVideoReleasePointTextElement = OwnView.Q<TextElement>("idle-video-release-point");
        AccumulatedPointTextElement = OwnView.Q<TextElement>("accumulated-point-text");
        
        ReturnVisualElement= OwnView.Q<VisualElement>("back-button");

    }
}