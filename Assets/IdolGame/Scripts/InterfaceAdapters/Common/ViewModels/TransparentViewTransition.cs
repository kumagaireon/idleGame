using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine.UIElements;

namespace IdolGame.Common.ViewModels;

public sealed class TransparentViewTransition: IViewTransition
{
    const float Transparent = 0.0f;
    const float Opaque = 1.0f;
    const float Duration = 0.08f;

    readonly VisualElement ownView;

    public TransparentViewTransition(VisualElement ownView)
    {
        this.ownView = ownView;
    }

    public void AddTransition()
    {
    }

    public void RemoveTransition()
    {
    }

    public async UniTask TransitionInAsync(SceneTransitionState state, CancellationToken ct)
    {
        ownView.style.opacity = Transparent;
        await LMotion.Create(Transparent, Opaque, Duration)
            .Bind(x => ownView.style.opacity = x)
            .ToUniTask(ct);
    }

    public async UniTask TransitionOutAsync(SceneTransitionState state, CancellationToken ct)
    {
        await LMotion.Create(Opaque, Transparent, Duration)
            .Bind(x => ownView.style.opacity = x)
            .ToUniTask(ct);
    }

    public void Dispose()
    {
    }
}