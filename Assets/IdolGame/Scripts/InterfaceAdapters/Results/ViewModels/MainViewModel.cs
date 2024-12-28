using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.Audios.Core;
using IdolGame.Common.infrastructures;
using IdolGame.Common.ViewModels;
using IdolGame.Results.Views;
using IdolGame.UIElements;
using Microsoft.Extensions.Logging;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using ZLogger;

namespace IdolGame.Results.ViewModels;

public sealed class MainViewModel: ViewModelBase<MainView>
{
    // ログ記録用のロガー
    readonly ILogger<MainViewModel> logger;
    DisposableBag bag;
    readonly AudioPlayer audioPlayer;
    readonly AssetReference bgmAssetReference;
    
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
        logger.ZLogTrace($"Called {GetType().Name}.InitializeAsync");
        //インゲームの結果ポイントを受け取る
        //仮置き
        float kariInGameResult = 100.0f;
        
        // グループの背景画像を設定
        if (!string.IsNullOrEmpty(GlobalState.GroupBackgroundImagePath))
        {
            var backgroundHandle = Addressables.LoadAssetAsync<Texture2D>(GlobalState.GroupBackgroundImagePath);
            await backgroundHandle.Task;
            if (backgroundHandle.Status == AsyncOperationStatus.Succeeded)
            {
                view.BackgroundImageVisualElement.style.backgroundImage = new StyleBackground(backgroundHandle.Result);
            }
            else
            {
                logger.ZLogError($"Failed to load background image: {GlobalState.GroupBackgroundImagePath}");
            }
        }
        
        // アイドルの画像を設定
        if (!string.IsNullOrEmpty(GlobalState.IdolImagePath))
        {
            var idolImageHandle = Addressables.LoadAssetAsync<Texture2D>(GlobalState.IdolImagePath);
            await idolImageHandle.Task;
            if (idolImageHandle.Status == AsyncOperationStatus.Succeeded)
            {
                view.IdolImageVisualElement.style.backgroundImage = new StyleBackground(idolImageHandle.Result);
            }
            else
            {
                logger.ZLogError($"Failed to load idol image: {GlobalState.IdolImagePath}");
            }
        }

        // 結果ポイントを表示
      //  view.ResultPointsTextElement.text = poinat;
        
        // リザルトのSABC別の必要ポイントと比較
        if (GlobalState.IdolResultRankPoint != null)
        {
            for (int i = 0; i < GlobalState.IdolResultRankPoint.Count; i++)
            {
                float point = GlobalState.IdolResultRankPoint[i];
                if (kariInGameResult < point) continue;

                string resultVoice = GlobalState.IdolResultRankVoice?[i] ?? "Unknown voice";
                string resultText = GlobalState.IdolResultRankText?[i] ?? "Unknown text";
              
                logger.ZLogInformation(
                    $"Reached rank with {point} points. Voice: {resultVoice}, Text: {resultText}");
                
                view.IdolSupportDialogueTextElement.text = resultText;
                break;
            }
        }

        //手持ちポイントに追加する
        GlobalState.IdolRewardPoint += kariInGameResult;
        
        view.MenuVisualElement.OnInputAsObservable()
            .SubscribeAwait(async (e, ct2)
                => await OnInputMenu(e, ct2))
            .AddTo(ref bag);

        view.RetryVisualElement.OnInputAsObservable()
            .SubscribeAwait(async (e, ct2)
                => await OnInputRetry(e, ct2))
            .AddTo(ref bag);


        // 非同期処理のためにフレームを待機
        await UniTask.Yield(ct);
    }

    async UniTask OnInputMenu(PointerDownEvent e, CancellationToken ct)
    {
        logger.ZLogInformation($"メニュー画面に戻る");
        await audioPlayer.StopBgmAsync(bgmAssetReference, ct);
        await SceneManager.LoadSceneAsync("MenuScene")!.WithCancellation(ct);
    }
    async UniTask OnInputRetry(PointerDownEvent e, CancellationToken ct)
    {
        logger.ZLogInformation($"リトライ");
        await audioPlayer.StopBgmAsync(bgmAssetReference, ct);
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