using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Audios.Core;
using IdolGame.Common.ViewModels;
using IdolGame.Gallery.ViewModels;
using Microsoft.Extensions.Logging;
using UnityEngine.AddressableAssets;
using VContainer.Unity;
using ZLogger;

namespace IdolGame.Gallery.Presenter;

public sealed class GalleryPresenter:IAsyncStartable
{
    readonly ILogger<GalleryPresenter> logger;
    readonly MainViewModel mainViewModel;
    readonly AudioPlayer audioPlayer;
    readonly AssetReference bgmAssetReference;

    public GalleryPresenter(ILogger<GalleryPresenter> logger,
        MainViewModel mainViewModel, 
        AudioPlayer audioPlayer, AssetReference bgmAssetReference)
    {
        this.logger = logger;
        this.mainViewModel = mainViewModel;
        this.audioPlayer = audioPlayer;
        this.bgmAssetReference = bgmAssetReference;
    }

    public async UniTask StartAsync(CancellationToken ct)
    {
        logger.ZLogTrace($"Called {GetType().Name}.StartAsync");

        mainViewModel.AddView(true);

        await audioPlayer.InitializeAsync(ct);
        
        await mainViewModel.InitializeAsync(ct);
        await UniTask.WaitForSeconds((1.0f), cancellationToken: ct);
       
        await audioPlayer.PlayBgmAsync(bgmAssetReference, ct);
        await mainViewModel.OpenaAsync(SceneTransitionState.Next, ct);
    }
}