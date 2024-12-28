using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Audios.Core;
using IdolGame.Common.ViewModels;
using IdolGame.Menu.ViewModels;
using Microsoft.Extensions.Logging;
using UnityEngine.AddressableAssets;
using VContainer.Unity;
using ZLogger;

namespace IdolGame.Menu.Presenter;

public sealed class MenuPresenter : IAsyncStartable
{
    readonly ILogger<MenuPresenter> logger;

    // メインビューのビューモデル
    readonly MainViewModel mainViewModel;

  
    readonly AudioPlayer audioPlayer;
    readonly AssetReference bgmAssetReference;

    public MenuPresenter(ILogger<MenuPresenter> logger,
        MainViewModel mainViewModel,
        AudioPlayer audioPlayer,
        AssetReference bgmAssetReference)
    {
        this.logger = logger;
        this.mainViewModel = mainViewModel;
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
        
       
        // メインビューの初期化
        await mainViewModel.InitializeAsync(ct);
        // 一定時間待機（1秒）
        await UniTask.WaitForSeconds((1.0f), cancellationToken: ct);
        
        await audioPlayer.PlayBgmAsync(bgmAssetReference, ct);
        // ビューを開く
        await mainViewModel.OpenWithoutAddAsync(SceneTransitionState.Next, ct);
    }

}