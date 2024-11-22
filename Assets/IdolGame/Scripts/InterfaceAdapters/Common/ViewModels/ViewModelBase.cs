using System;
using System.Threading;
using IdolGame.Common.Views;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

namespace IdolGame.Common.ViewModels;

public abstract class ViewModelBase<TView> : IDisposable where TView : ViewBase
{
    protected readonly TView view;
    readonly UIDocument rootDocument;
    readonly IViewTransition transition;

    protected ViewModelBase(TView view, UIDocument rootDocument, IViewTransition transition)
    {
        this.view = view;
        this.rootDocument = rootDocument;
        this.transition = transition;
    }

    public async UniTask OpenAsync(SceneTransitionState state, CancellationToken ct)
    {
        AddView();
        PreOpen();
        await transition.TransitionInAsync(state, ct);
    }

    public async UniTask CloseAsync(SceneTransitionState state, CancellationToken ct)
    {
        await transition.TransitionOutAsync(state, ct);
        RemoveView();
    }

    public async UniTask OpenWithoutAddAsync(SceneTransitionState state, CancellationToken ct)
    {
        PreOpen();
        await transition.TransitionInAsync(state, ct);
    }

    public async UniTask CloseWithoutRemoveAsync(SceneTransitionState state, CancellationToken ct)
    {
        await transition.TransitionOutAsync(state, ct);
    }

    public void AddView(bool addedTransition = false)
    {
        rootDocument.rootVisualElement.Add(view.OwnView);
        if (addedTransition)
        {
            transition.AddTransition();
        }
    }

    public void RemoveView(bool removedTransition = false)
    {
        rootDocument.rootVisualElement.Remove(view.OwnView);
        if (removedTransition)
        {
            transition.RemoveTransition();
        }
    }

    public void Dispose()
    {
        transition.Dispose();
        OnDispose();
    }

    protected abstract void PreOpen();
    protected abstract void OnDispose();
}