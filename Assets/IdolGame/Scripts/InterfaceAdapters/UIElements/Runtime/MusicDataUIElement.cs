using UnityEngine.UIElements;

namespace IdolGame.UIElements;

[UxmlElement]
public partial class MusicDataUIElement : VisualElement
{
    readonly VisualElement background = new() { name = "background" };
    readonly VisualElement musicImageElement = new() { name = "music-image" };
    readonly TextElement musicNameElement = new() { name = "music-name" };
    
    public int Index { get; set; }
    
    // コンストラクタ
    public MusicDataUIElement()
    {
        // ビューに背景エレメントを追加
        Add(background);
        
        // 背景エレメントに音楽画像エレメントを追加
        background.Add(musicImageElement);
        
        // 背景エレメントに音楽名エレメントを追加
        background.Add(musicNameElement);
    }
}