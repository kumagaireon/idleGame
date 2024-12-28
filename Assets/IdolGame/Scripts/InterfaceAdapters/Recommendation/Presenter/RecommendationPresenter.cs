using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Audios.Core;
using IdolGame.Common.ViewModels;
using IdolGame.Recommendation.ViewModels;
using Microsoft.Extensions.Logging;
using UnityEngine.AddressableAssets;
using VContainer.Unity;
using ZLogger;

namespace IdolGame.Recommendation.Presenter;

public sealed class RecommendationPresenter : IAsyncStartable
{
    readonly ILogger<RecommendationPresenter> logger;
    readonly MainViewModel mainViewModel;
    readonly IAudioPlayerService audioPlayerService;
    readonly AssetReference bgmAssetReference;

    public RecommendationPresenter(ILogger<RecommendationPresenter> logger, MainViewModel mainViewModel,
        IAudioPlayerService audioPlayerService, AssetReference bgmAssetReference)
    {
        this.logger = logger;
        this.mainViewModel = mainViewModel;
        this.audioPlayerService = audioPlayerService;
        this.bgmAssetReference = bgmAssetReference;
    }

    public async UniTask StartAsync(CancellationToken ct)
    {
        logger.ZLogTrace($"Called {GetType().Name}.StartAsync");
        mainViewModel.AddView(true);
        await audioPlayerService.InitializeAsync(ct);
        await mainViewModel.InitializeAsync(ct);
        await UniTask.WaitForSeconds(1.0f, cancellationToken: ct);
        await audioPlayerService.PlayBgmAsync(bgmAssetReference, 1.0f, ct);
        await mainViewModel.OpenWithoutAddAsync(SceneTransitionState.Next, ct);
    }
}