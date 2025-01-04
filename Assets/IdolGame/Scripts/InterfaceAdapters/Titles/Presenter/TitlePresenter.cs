using System.Threading;
using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Common.ViewModels;
using IdolGame.Titles.ViewModels;
using Cysharp.Threading.Tasks;
using IdolGame.Audios.Core;
using Microsoft.Extensions.Logging;
using UnityEngine.AddressableAssets;
using VContainer.Unity;
using ZLogger;

namespace IdolGame.Titles.Presenter;

/*
public struct CommonAudioReference
{
    public AssetReference bgmAssetReference;
   public AssetReference seAssetReference;
}
   */

/// <summary>
/// タイトル画面のプレゼンタークラス
/// </summary>
public sealed class TitlePresenter : IAsyncStartable
{
    // ログ記録用のロガー
    readonly ILogger<TitlePresenter> logger;

    // メインビューのビューモデル
    readonly MainViewModel mainViewModel;
  
    readonly AudioPlayer audioPlayer;
    readonly AssetReference bgmAssetReference;

 
    public TitlePresenter(ILogger<TitlePresenter> logger,
        MainViewModel mainViewModel,  AudioPlayer audioPlayer,
        AssetReference bgmAssetReference)
    {
        this.logger = logger;
        this.mainViewModel = mainViewModel;
        this.audioPlayer = audioPlayer;
        this.bgmAssetReference = bgmAssetReference;
    }

   
    public async UniTask StartAsync(CancellationToken ct)
    {
        // ログ出力：メソッド開始
        logger.ZLogTrace($"Called {GetType().Name}.StartAsync");
        // ビューの追加
        await mainViewModel.ShowImageAndFadeOutAsync(mainViewModel.view.CompanyVisualElement,1.0f, 2.0f, ct);
        
        await audioPlayer.InitializeAsync(ct);
       
        await audioPlayer.PlayBgmAsync(bgmAssetReference, ct);
        // メインビューの初期化
        await mainViewModel.InitializeAsync(ct);
        
        // ビューを開く
        await mainViewModel.OpenaAsync(SceneTransitionState.Next, ct);
       
    }
}