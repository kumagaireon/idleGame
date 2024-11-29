using System.Threading;
using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Common.ViewModels;
using IdolGame.Titles.ViewModels;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VContainer.Unity;
using ZLogger;

namespace IdolGame.Titles.Presenter;

/// <summary>
/// タイトル画面のプレゼンタークラス
/// </summary>
public sealed class TitlePresenter: IAsyncStartable
{   
    // ログ記録用のロガー
    readonly ILogger<TitlePresenter> logger;
    // メインビューのビューモデル
    readonly MainViewModel mainViewModel;
    // セーブデータを取得するユースケース
    readonly FindSaveDataUseCase findSaveDataUseCase;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガーのインスタンス</param>
    /// <param name="mainViewModel">メインビューのビューモデル</param>
    /// <param name="findSaveDataUseCase">セーブデータ取得ユースケース</param>
    public TitlePresenter(ILogger<TitlePresenter> logger,
        MainViewModel mainViewModel, FindSaveDataUseCase findSaveDataUseCase)
    {
        this.logger = logger;
        this.mainViewModel = mainViewModel;
        this.findSaveDataUseCase = findSaveDataUseCase;
    }
    
    /// <summary>
    /// 非同期にプレゼンターを初期化するメソッド
    /// </summary>
    /// <param name="ct">キャンセルトークン</param>
    public async UniTask StartAsync(CancellationToken ct)
    {
        // ログ出力：メソッド開始
        logger.ZLogTrace($"Called {GetType().Name}.StartAsync");
         // ビューの追加
        mainViewModel.AddView(true);
        // セーブデータを非同期に取得
        var saves =await findSaveDataUseCase.FindAllAsync(ct);
        foreach (var save in saves)
        {
            logger.ZLogTrace($"{save}");// ログ出力：セーブデータの情報
        }
        // メインビューの初期化
        await mainViewModel.InitializeAsync(ct);
        // 一定時間待機（1秒）
        await UniTask.WaitForSeconds((1.0f), cancellationToken: ct);
        // ビューを開く
        await mainViewModel.OpenWithoutAddAsync(SceneTransitionState.Next, ct);
    }
}