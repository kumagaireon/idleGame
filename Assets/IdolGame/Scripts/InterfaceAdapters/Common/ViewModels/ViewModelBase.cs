using System;
using System.Threading;
using IdolGame.Common.Views;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

namespace IdolGame.Common.ViewModels;

/// <summary>
/// ビューの遷移を管理するための抽象基底クラス
/// </summary>
/// <typeparam name="TView">ビューの型</typeparam>
public abstract class ViewModelBase<TView> : IDisposable where TView : ViewBase
{
    protected readonly TView view;// 継承先で使用するビューのインスタンス
    readonly UIDocument rootDocument;// ルートUIDocument
    readonly IViewTransition transition;// ビューの遷移インターフェース

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="view">ビューのインスタンス</param>
    /// <param name="rootDocument">ルートUIDocument</param>
    /// <param name="transition">ビューの遷移インターフェース</param>
    protected ViewModelBase(TView view, UIDocument rootDocument, IViewTransition transition)
    {
        this.view = view;
        this.rootDocument = rootDocument;
        this.transition = transition;
    }

    /// <summary>
    /// ビューを表示し、遷移を非同期に実行
    /// </summary>
    /// <param name="state">シーン遷移の状態</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>非同期タスク</returns>
    public async UniTask OpenAsync(SceneTransitionState state, CancellationToken ct)
    {
        AddView();// ビューを追加
        PreOpen();// ビューを開く前の処理
        await transition.TransitionInAsync(state, ct);// 遷移アニメーションを実行
    }

    /// <summary>
    /// ビューを閉じ、遷移を非同期に実行
    /// </summary>
    /// <param name="state">シーン遷移の状態</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>非同期タスク</returns>
    public async UniTask CloseAsync(SceneTransitionState state, CancellationToken ct)
    {
        await transition.TransitionOutAsync(state, ct);// 遷移アニメーションを実行
        RemoveView();// ビューを削除
    }

    /// <summary>
    /// ビューを追加せずに遷移を非同期に実行
    /// </summary>
    /// <param name="state">シーン遷移の状態</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>非同期タスク</returns>
    public async UniTask OpenWithoutAddAsync(SceneTransitionState state, CancellationToken ct)
    {
        PreOpen();// ビューを開く前の処理
        await transition.TransitionInAsync(state, ct);// 遷移アニメーションを実行
    }

    /// <summary>
    /// ビューを削除せずに遷移を非同期に実行
    /// </summary>
    /// <param name="state">シーン遷移の状態</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>非同期タスク</returns>
    public async UniTask CloseWithoutRemoveAsync(SceneTransitionState state, CancellationToken ct)
    {
        await transition.TransitionOutAsync(state, ct);
    }

    /// <summary>
    /// ビューを追加
    /// </summary>
    /// <param name="addedTransition">遷移要素の追加フラグ</param>
    public void AddView(bool addedTransition = false)
    {
        rootDocument.rootVisualElement.Add(view.OwnView);
        if (addedTransition)
        {
            transition.AddTransition();
        }
    }

    /// <summary>
    /// ビューを削除
    /// </summary>
    /// <param name="removedTransition">遷移要素の削除フラグ</param>
    public void RemoveView(bool removedTransition = false)
    {
        rootDocument.rootVisualElement.Remove(view.OwnView);
        if (removedTransition)
        {
            transition.RemoveTransition();
        }
    }

    /// <summary>
    /// オブジェクトを破棄
    /// </summary>
    public void Dispose()
    {
        transition.Dispose();// 遷移インターフェースのリソースを解放
        OnDispose();// 派生クラスのリソースを解放する処理
    }

    /// <summary>
    /// ビューを開く前の処理を定義する抽象メソッド
    /// </summary>
    protected abstract void PreOpen();
    
    /// <summary>
    /// 派生クラスのリソースを解放するための抽象メソッド
    /// </summary>
    protected abstract void OnDispose();
}