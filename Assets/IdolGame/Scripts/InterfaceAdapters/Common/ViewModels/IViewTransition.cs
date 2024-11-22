using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace IdolGame.Common.ViewModels;

public interface IViewTransition : IDisposable
{
    void AddTransition();
    void RemoveTransition();
    UniTask TransitionInAsync(SceneTransitionState state, CancellationToken ct);
    UniTask TransitionOutAsync(SceneTransitionState state, CancellationToken ct);
}