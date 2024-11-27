using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace IdolGame.Common.ViewModels;

/// <summary>
/// ビューの遷移を定義するインターフェース
/// </summary>
public interface IViewTransition : IDisposable
{
    /// <summary>
    /// 遷移要素を追加
    /// </summary>
    void AddTransition();
    
    /// <summary>
    /// 遷移要素を削除
    /// </summary>
    void RemoveTransition();
    
    /// <summary>
    /// フェードイン遷移を非同期に実行
    /// </summary>
    /// <param name="state">シーン遷移の状態</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>非同期タスク</returns>
    UniTask TransitionInAsync(SceneTransitionState state, CancellationToken ct);
    
    /// <summary>
    /// フェードアウト遷移を非同期に実行
    /// </summary>
    /// <param name="state">シーン遷移の状態</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>非同期タスク</returns>
    UniTask TransitionOutAsync(SceneTransitionState state, CancellationToken ct);
}