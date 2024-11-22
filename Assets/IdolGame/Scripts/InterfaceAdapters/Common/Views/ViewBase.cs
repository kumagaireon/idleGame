using UnityEditor.VersionControl;
using UnityEngine.UIElements;

namespace IdolGame.Common.Views;

public abstract class ViewBase
{
    public VisualElement OwnView { get; }

    protected ViewBase(VisualTreeAsset asset)
    {
        OwnView = asset.Instantiate();
    }
}