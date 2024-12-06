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
    readonly FindSaveDataUseCase findSaveDataUseCase;
    readonly AudioPlayer audioPlayer;
    readonly AssetReference bgmAssetReference;

    public GalleryPresenter(ILogger<GalleryPresenter> logger,
        MainViewModel mainViewModel, FindSaveDataUseCase findSaveDataUseCase, 
        AudioPlayer audioPlayer, AssetReference bgmAssetReference)
    {
        this.logger = logger;
        this.mainViewModel = mainViewModel;
        this.findSaveDataUseCase = findSaveDataUseCase;
        this.audioPlayer = audioPlayer;
        this.bgmAssetReference = bgmAssetReference;
    }

    public async UniTask StartAsync(CancellationToken ct)
    {
        logger.ZLogTrace($"Called {GetType().Name}.StartAsync");

        mainViewModel.AddView(true);

        await audioPlayer.InitializeAsync(ct);
        
        var saves =await findSaveDataUseCase.FindAllAsync(ct);
        foreach (var save in saves)
        {
            logger.ZLogTrace($"{save}");
        }
        await mainViewModel.InitializeAsync(ct);
        await UniTask.WaitForSeconds((1.0f), cancellationToken: ct);
       
        await audioPlayer.PlayBgmAsync(bgmAssetReference, ct);
        await mainViewModel.OpenWithoutAddAsync(SceneTransitionState.Next, ct);
    }
}