using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.Common.ViewModels;
using IdolGame.Titles.Views;
using IdolGame.UIElements;
using Microsoft.Extensions.Logging;
using R3;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using ZLogger;

namespace IdolGame.Titles.ViewModels;

/// <summary>
/// タイトル画面のメインビューモデルクラス
/// </summary>
public sealed class MainViewModel: ViewModelBase<MainView>
{
    // ログ記録用のロガー
    readonly ILogger<MainViewModel> logger;

    DisposableBag bag;
    
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガーのインスタンス</param>
    /// <param name="view">ビューのインスタンス</param>
    /// <param name="rootDocument">ルートドキュメントのインスタンス</param>
    public MainViewModel(ILogger<MainViewModel> logger,
        MainView view,
        UIDocument rootDocument)
        : base(view, rootDocument, new FadeViewTransition(rootDocument))
    {
        this.logger = logger;
    }

    /// <summary>
    /// 非同期にビューを初期化するメソッド
    /// </summary>
    /// <param name="ct">キャンセルトークン</param>
    public async UniTask InitializeAsync(CancellationToken ct)
    {
        view.TouchPanel.OnInputAsObservable()
            .SubscribeAwait(async (e, ct2)
                => await OnInput(e, ct2)).AddTo(ref bag);
        
        // 非同期処理のためにフレームを待機
        await UniTask.Yield(ct);
    }

 

    async UniTask OnInput(PointerDownEvent e,CancellationToken ct)
    {
        logger.ZLogInformation($"OnMouseDown");
        await SceneManager.LoadSceneAsync("MenuScene")!.WithCancellation(ct);
    }

    /// <summary>
    /// ビューが開く前に実行される処理
    /// </summary>
    protected override void PreOpen()
    {
    }

    /// <summary>
    /// ビューの破棄時に実行される処理
    /// </summary>
    protected override void OnDispose()
    {
        bag.Dispose();
    }
}