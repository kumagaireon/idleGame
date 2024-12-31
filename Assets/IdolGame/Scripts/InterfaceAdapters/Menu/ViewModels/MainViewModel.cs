using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.Audios.Core;
using IdolGame.Common.infrastructures;
using IdolGame.Common.ViewModels;
using IdolGame.Menu.Views;
using IdolGame.UIElements;
using Microsoft.Extensions.Logging;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using ZLogger;

namespace IdolGame.Menu.ViewModels;

public sealed class MainViewModel : ViewModelBase<MainView>
{

    // ログ記録用のロガー
    readonly ILogger<MainViewModel> logger;
    readonly AudioPlayer audioPlayer;
    readonly AssetReference bgmAssetReference;
    DisposableBag bag;

    public MainViewModel(ILogger<MainViewModel> logger,
        MainView view,
        UIDocument rootDocument,
        AudioPlayer audioPlayer,
        AssetReference bgmAssetReference)
        : base(view, rootDocument, new FadeViewTransition(rootDocument))
    {
        this.logger = logger;
        this.audioPlayer = audioPlayer;
        this.bgmAssetReference = bgmAssetReference;
    }


    public async UniTask InitializeAsync(CancellationToken ct)
    {
        logger.ZLogTrace($"Called {GetType().Name}.InitializeAsync");

        await SetUiElementsFromGlobalState();

        view. OptionVisualElement.OnInputAsObservable()
            .SubscribeAwait(async (e, ct2)
                => await OnInputOption(e, ct2))
            .AddTo(ref bag);
        
        view.SongSelectionVisualElement.OnInputAsObservable()
            .SubscribeAwait(async (e, ct2)
                => await OnInputSongSelection(e, ct2))
            .AddTo(ref bag);

        view.AboutMeVisualElement.OnInputAsObservable()
            .SubscribeAwait(async (e, ct2)
                => await OnInputAboutMe(e, ct2))
            .AddTo(ref bag);

        // 非同期処理のためにフレームを待機
        await UniTask.Yield(ct);
    }

    async UniTask SetUiElementsFromGlobalState()
    {
        if (GlobalState.IdolId != null)
        {
            // GroupButtonUIPathを設定
            var groupButtonUIHandle =
                Addressables.LoadAssetAsync<Texture2D>(GlobalState.GroupButtonUIPath);
            await groupButtonUIHandle.Task;
            if (groupButtonUIHandle.Status == AsyncOperationStatus.Succeeded)
            {
                view.SongSelectionVisualElement.style.backgroundImage =
                    new StyleBackground(groupButtonUIHandle.Result);
                view.AboutMeVisualElement.style.backgroundImage =
                    new StyleBackground(groupButtonUIHandle.Result);
            }
            else
            {
                logger.ZLogError($"Failed to load texture: {GlobalState.GroupButtonUIPath}");

            }

            // GroupBackgroundImagePathを設定
            var groupBackgroundImageHandle =
                Addressables.LoadAssetAsync<Texture2D>(GlobalState.GroupBackgroundImagePath);
            await groupBackgroundImageHandle.Task;
            if (groupBackgroundImageHandle.Status == AsyncOperationStatus.Succeeded)
            {
                view.BackgroundImageVisualElement.style.backgroundImage =
                    new StyleBackground(groupBackgroundImageHandle.Result);
            }
            else
            {
                logger.ZLogError($"Failed to load texture: {GlobalState.GroupBackgroundImagePath}");
            }

            // IdolImagePathを設定
            var idolImageHandle =
                Addressables.LoadAssetAsync<Texture2D>(GlobalState.IdolImagePath);

            await idolImageHandle.Task;
            if (idolImageHandle.Status == AsyncOperationStatus.Succeeded)
            {
                view.IdolImageVisualElement.style.backgroundImage =
                    new StyleBackground(idolImageHandle.Result);
            }
            else
            {
                logger.ZLogError($"Failed to load texture: {GlobalState.IdolImagePath}");
            }

            // IdolSerifMenuTextを設定
            view.IdolSpeechBubbleTextElement.text = GlobalState.IdolSerifMenuText;

            view.SpeechBubbleVisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            view.SpeechBubbleVisualElement.style.display = DisplayStyle.None;
        }
    }

    async UniTask OnInputOption(PointerDownEvent e, CancellationToken ct)
    {
        logger.ZLogInformation($"オプション画面遷移");
        await audioPlayer.StopBgmAsync(bgmAssetReference, ct);
    }

    async UniTask OnInputSongSelection(PointerDownEvent e, CancellationToken ct)
    {
        logger.ZLogInformation($"選曲画面遷移");
        await audioPlayer.StopBgmAsync(bgmAssetReference, ct);
        await SceneManager.LoadSceneAsync("SelectScene")!.WithCancellation(ct);
    }

    async UniTask OnInputAboutMe(PointerDownEvent e, CancellationToken ct)
    {
        logger.ZLogInformation($"自己紹介画面遷移");
        await audioPlayer.StopBgmAsync(bgmAssetReference, ct);
        await SceneManager.LoadSceneAsync("OsiSetupScene")!.WithCancellation(ct);
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