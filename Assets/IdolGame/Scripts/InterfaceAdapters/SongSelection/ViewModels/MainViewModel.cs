using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.Audios.Core;
using IdolGame.Common.ViewModels;
using IdolGame.SongSelection.Views;
using IdolGame.UIElements;
using Microsoft.Extensions.Logging;
using R3;
using UIToolkit.R3.Integration;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Video;
using ZLogger;

namespace IdolGame.SongSelection.ViewModels;

public sealed class MainViewModel: ViewModelBase<MainView>
{
    // ログ記録用のロガー
    readonly ILogger<MainViewModel> logger;
    DisposableBag bag;
    readonly AudioPlayer audioPlayer;
    readonly AssetReference bgmAssetReference;
    string? selectedVoicePath;
    
    public Func<SceneTransitionState, CancellationToken, UniTask>? CloseContinueAsync { get; set; }

    public MainViewModel(ILogger<MainViewModel> logger,
        MainView view,
        UIDocument rootDocument, AudioPlayer audioPlayer, AssetReference bgmAssetReference)
        : base(view, rootDocument, new FadeViewTransition(rootDocument))
    {
        this.logger = logger;
        this.audioPlayer = audioPlayer;
        this.bgmAssetReference = bgmAssetReference;
    }

    public async UniTask InitializeAsync(CancellationToken ct)
    {
        logger.ZLogTrace($"Called {GetType().Name}.InitializeAsync");

        var scrollView = view.SongSelectionScrollView as CustomScrollView;

        if (scrollView == null)
        {
            logger.ZLogError($"SongSelectionScrollView is null.");
            return;
        }


        // JSON データを読み込み
        var musicDataLoader = new MusicDataLoader();
        var musicsong = await musicDataLoader.LoadMusicDataAsync();


        if (musicsong == null)
            return;

        var firstSong = musicsong.First();
        var handle = Addressables.LoadAssetAsync<Texture2D>(firstSong.ImagePath.ToString());
        await handle.Task;
        
        view.ExplanationSongSelectionTextElement.text = firstSong.Description;
        view.MusicJacketVisualElement.style.backgroundImage = new StyleBackground(handle.Result);
        selectedVoicePath = firstSong.VoicePath;
      
        foreach (var song in musicsong)
        {
            var element = scrollView.itemsTemplate.Instantiate();
            element.Q<Label>("song-name").text = song.Name.ToString();

            element.OnInputAsObservable()
                .SubscribeAwait(async (evt, ct2) =>
                    await OnInput(evt, ct2, song))
                .AddTo(ref bag);

            scrollView.Add(element);
        }

        view.StartVisualElement.OnInputAsObservable()
            .SubscribeAwait(async (e, ct2)
                => await OnInputStart(e, ct2))
            .AddTo(ref bag);

        view.ReturnVisualElement.OnInputAsObservable()
            .SubscribeAwait(async (e, ct2)
                => await OnInputReturn(e, ct2))
            .AddTo(ref bag);

        // 非同期処理のためにフレームを待機
        await UniTask.Yield(ct);
    }

    async UniTask OnInput(PointerDownEvent e, CancellationToken ct,MusicData musicData)
    {
        var handle = Addressables.LoadAssetAsync<Texture2D>(musicData.ImagePath.ToString());
        await handle.Task;
        
        view.ExplanationSongSelectionTextElement.text= musicData.Description;
        view.MusicJacketVisualElement.style.backgroundImage = new StyleBackground(handle.Result);

        selectedVoicePath = musicData.VoicePath;
        logger.ZLogTrace($"{selectedVoicePath}");
        await UniTask.Yield(ct);
    }

    async UniTask OnInputStart(PointerDownEvent e, CancellationToken ct)
    {
        logger.ZLogInformation($"インゲーム画面遷移");

        if (selectedVoicePath != null)
        {
            var voiceHandle = Addressables.LoadAssetAsync<VideoClip>(selectedVoicePath);
            await voiceHandle.Task;
            if (voiceHandle.Status == AsyncOperationStatus.Succeeded)
            {
                //==================================
                //ここでInGameの方にvoiceHandleを渡したい
                //==================================
                
                if (CloseContinueAsync != null)
                {
                    await CloseContinueAsync(SceneTransitionState.Next, ct);
                   
                }
                await audioPlayer.StopBgmAsync(bgmAssetReference, ct);
                await SceneManager.LoadSceneAsync("ResultScene")!.WithCancellation(ct);
            }
        }
        else
        {
            logger.ZLogTrace($"ないよ");
        }
        
    }

    async UniTask OnInputReturn(PointerDownEvent e, CancellationToken ct)
    {
        logger.ZLogInformation($"メニュー画面に戻る");
        await audioPlayer.StopBgmAsync(bgmAssetReference, ct);
        await SceneManager.LoadSceneAsync("MenuScene")!.WithCancellation(ct);
    }
    
    /// <summary>
    /// ビューが開く前に実行される処理
    /// </summary>
    public override void PreOpen()
    {
        view.SongSelectionScrollView.Focus();
    }

    /// <summary>
    /// ビューの破棄時に実行される処理
    /// </summary>
    protected override void OnDispose()
    {
        bag.Dispose();
    }
}