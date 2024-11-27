using UnityEditor.VersionControl;
using UnityEngine.UIElements;

namespace IdolGame.Common.Views;

public abstract class ViewBase//ビューの基本クラス
{
    // このビューに関連付けられたVisualElement
    public VisualElement OwnView { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>  
    /// <param name="asset">VisualTreeAssetのインスタンス</param>
    protected ViewBase(VisualTreeAsset asset)
    {
        OwnView = asset.Instantiate();// VisualTreeAssetからVisualElementをインスタンス化
    }
}