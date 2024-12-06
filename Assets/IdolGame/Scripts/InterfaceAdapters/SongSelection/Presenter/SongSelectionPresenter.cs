using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Audios.Core;
using IdolGame.Common.ViewModels;
using IdolGame.SongSelection.ViewModels;
using Microsoft.Extensions.Logging;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer.Unity;
using ZLogger;

namespace IdolGame.SongSelection.Presenter;

public sealed class SongSelectionPresenter: IAsyncStartable

{
    // ログ記録用のロガー
    readonly ILogger<SongSelectionPresenter> logger;

    // メインビューのビューモデル
    readonly MainViewModel mainViewModel;

    // セーブデータを取得するユースケース
    readonly FindSaveDataUseCase findSaveDataUseCase;
  
    readonly AudioPlayer audioPlayer;
    readonly AssetReference bgmAssetReference;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガーのインスタンス</param>
    /// <param name="mainViewModel">メインビューのビューモデル</param>
    /// <param name="findSaveDataUseCase">セーブデータ取得ユースケース</param>
    public SongSelectionPresenter(ILogger<SongSelectionPresenter> logger,
        MainViewModel mainViewModel, FindSaveDataUseCase findSaveDataUseCase, AudioPlayer audioPlayer,
        AssetReference bgmAssetReference)
    {
        this.logger = logger;
        this.mainViewModel = mainViewModel;
        this.findSaveDataUseCase = findSaveDataUseCase;
        this.audioPlayer = audioPlayer;
        this.bgmAssetReference = bgmAssetReference;
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
        await audioPlayer.InitializeAsync(ct);
        
        // セーブデータを非同期に取得
        var saves = await findSaveDataUseCase.FindAllAsync(ct);
        foreach (var save in saves)
        {
            logger.ZLogTrace($"{save}"); // ログ出力：セーブデータの情報
        }

        // メインビューの初期化
        await mainViewModel.InitializeAsync(ct);
        // 一定時間待機（1秒）
        await UniTask.WaitForSeconds((1.0f), cancellationToken: ct);
        await audioPlayer.PlayBgmAsync(bgmAssetReference, ct);
        // ビューを開く
        await mainViewModel.OpenWithoutAddAsync(SceneTransitionState.Next, ct);
    }
}