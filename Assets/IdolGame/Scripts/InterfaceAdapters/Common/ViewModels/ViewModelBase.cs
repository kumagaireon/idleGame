using System;
using System.Threading;
using IdolGame.Common.Views;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

namespace IdolGame.Common.ViewModels;

// ビューの遷移を管理するための抽象基底クラス
public abstract class ViewModelBase<TView> : IDisposable where TView : ViewBase
{
    public readonly TView view; // 継承先で使用するビューのインスタンス
    readonly UIDocument rootDocument; // ルートUIDocument
    readonly IViewTransition transition; // ビューの遷移インターフェース

    // コンストラクタ
    protected ViewModelBase(TView view, UIDocument rootDocument, IViewTransition transition)
    {
        this.view = view;
        this.rootDocument = rootDocument;
        this.transition = transition;
    }

    // ビューを表示し、遷移を非同期に実行
    public async UniTask OpenAsync(SceneTransitionState state, CancellationToken ct)
    {
        AddView(); // ビューを追加
        PreOpen(); // ビューを開く前の処理
        await transition.TransitionInAsync(state, ct); // 遷移アニメーションを実行
    }

    // ビューを閉じ、遷移を非同期に実行
    public async UniTask CloseAsync(SceneTransitionState state, CancellationToken ct)
    {
        await transition.TransitionOutAsync(state, ct); // 遷移アニメーションを実行
        RemoveView(); // ビューを削除
    }

    // ビューを追加せずに遷移を非同期に実行
    public async UniTask OpenWithoutAddAsync(SceneTransitionState state, CancellationToken ct)
    {
        PreOpen(); // ビューを開く前の処理
        await transition.TransitionInAsync(state, ct); // 遷移アニメーションを実行
    }

    public async UniTask OpenaAsync(SceneTransitionState state, CancellationToken ct)
    {
        PreOpen(); // ビューを開く前の処理
    }
    // ビューを削除せずに遷移を非同期に実行
    public async UniTask CloseWithoutRemoveAsync(SceneTransitionState state, CancellationToken ct)
    {
        await transition.TransitionOutAsync(state, ct);
    }

    // ビューを追加
    public void AddView(bool addedTransition = false)
    {
        rootDocument.rootVisualElement.Add(view.OwnView);
        if (addedTransition)
        {
            transition.AddTransition();
        }
    }

    // ビューを削除
    public void RemoveView(bool removedTransition = false)
    {
        rootDocument.rootVisualElement.Remove(view.OwnView);
        if (removedTransition)
        {
            transition.RemoveTransition();
        }
    }

    // オブジェクトを破棄
    public void Dispose()
    {
        transition.Dispose(); // 遷移インターフェースのリソースを解放
        OnDispose(); // 派生クラスのリソースを解放する処理
    }

    // ビューを開く前の処理を定義する抽象メソッド
    public abstract void PreOpen();

    // 派生クラスのリソースを解放するための抽象メソッド
    protected abstract void OnDispose();

    public async UniTask ShowImageAndFadeOutAsync(
        VisualElement visualElement, float fadeOutDuration,float DisplayTime,
        CancellationToken ct)
    {
        if (!rootDocument.rootVisualElement.Contains(view.OwnView))
        {
            rootDocument.rootVisualElement.Add(view.OwnView);
        }
        
        await UniTask.WaitForSeconds(DisplayTime, cancellationToken: ct);
        
        // フェードアウト前に画像を表示
        visualElement.style.opacity = 1;

        // フェードアウトの持続時間を待機
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += UnityEngine.Time.deltaTime;
            float opacity = UnityEngine.Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            visualElement.style.opacity = opacity;
            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }

        // フェードアウトの効果
        visualElement.style.opacity = 0;
        // 非同期処理のためにフレームを待機
        await UniTask.Yield(ct);
    }
}