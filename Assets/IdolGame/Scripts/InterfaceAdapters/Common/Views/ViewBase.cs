using UnityEditor.VersionControl;
using UnityEngine.UIElements;

namespace IdolGame.Common.Views;

public abstract class ViewBase//ビューの基本クラス
{
    // このビューに関連付けられたVisualElement
    public VisualElement OwnView { get; }
    
    protected ViewBase(VisualTreeAsset asset)
    {
        OwnView = asset.Instantiate();// VisualTreeAssetからVisualElementをインスタンス化
    }
}