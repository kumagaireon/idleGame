using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Cysharp.Threading.Tasks;
using IdolGame.Audios.Core;
using IdolGame.Common.infrastructures;
using IdolGame.Common.ViewModels;
using IdolGame.Recommendation;
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

public sealed class MainViewModel : ViewModelBase<MainView>
{
    // ログ記録用のロガー
    readonly ILogger<MainViewModel> logger;
    DisposableBag bag;
    readonly AudioPlayer audioPlayer;
    readonly AssetReference bgmAssetReference;
    List<string> resultImagePath = new List<string> { "s-image", "a-image", "b-image", "c-image" };
    int Index = 0;

    public MainViewModel(ILogger<MainViewModel> logger,
        MainView view,
        UIDocument rootDocument, AudioPlayer audioPlayer,
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
        //インゲームの結果ポイントを受け取る
        //仮置き
        float kariInGameResult = 1050.0f;

        var idolDataLoader = new IdolDataLoader();
        var idolGroups = await idolDataLoader.LoadIdolDataAsync();

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
        view.ResultPointsTextElement.text =
            ($"{kariInGameResult.ToString(CultureInfo.CurrentCulture)}ポイント");

        logger.ZLogTrace($"{kariInGameResult.ToString(CultureInfo.CurrentCulture)}ポイント");

        // GlobalState.IdolIdとidolGroupsのアイドルのIDが一致するアイドルデータのIdolPointにkariInGameResultを追加
        bool updated = false;
        foreach (var group in idolGroups)
        {
            if (group.Members == null) continue;
            for (int i = 0; i < group.Members.Length; i++)
            {
                var idol = group.Members[i];
                if (idol.Id == GlobalState.IdolId)
                {
                    var newIdolPoint = idol.IdolReward.IdolPoint + kariInGameResult;
                    var updatedReward = idol.IdolReward with { IdolPoint = newIdolPoint };
                    if (newIdolPoint >= idol.IdolReward.Cheki1Point)
                    {
                        updatedReward = updatedReward with { DateAcquisitioRewardCheck1 = DateTimeOffset.Now };
                    }

                    if (newIdolPoint >= idol.IdolReward.Cheki2Point)
                    {
                        updatedReward = updatedReward with { DateAcquisitioRewardCheck2 = DateTimeOffset.Now };
                    }

                    if (newIdolPoint >= idol.IdolReward.Cheki3Point)
                    {
                        updatedReward = updatedReward with { DateAcquisitioRewardCheck3 = DateTimeOffset.Now };
                    }

                    if (newIdolPoint >= idol.IdolReward.VoicePoint)
                    {
                        updatedReward = updatedReward with { DateAcquisitioRewardVideo = DateTimeOffset.Now };
                    }

                    group.Members[i] = idol with { IdolReward = updatedReward };
                    logger.ZLogTrace($"Updated {idol.Name} IdolPoint: {updatedReward.IdolPoint}");
                    logger.ZLogInformation($"Comparing idol points with reward thresholds for {idol.Name}:");
                    logger.ZLogInformation(
                        $"Cheki1Point: {idol.IdolReward.Cheki1Point}, NewIdolPoint: {newIdolPoint}, Exceeds: {newIdolPoint >= idol.IdolReward.Cheki1Point}");
                    logger.ZLogInformation(
                        $"Cheki2Point: {idol.IdolReward.Cheki2Point}, NewIdolPoint: {newIdolPoint}, Exceeds: {newIdolPoint >= idol.IdolReward.Cheki2Point}");
                    logger.ZLogInformation(
                        $"Cheki3Point: {idol.IdolReward.Cheki3Point}, NewIdolPoint: {newIdolPoint}, Exceeds: {newIdolPoint >= idol.IdolReward.Cheki3Point}");
                    logger.ZLogInformation(
                        $"VoicePoint: {idol.IdolReward.VoicePoint}, NewIdolPoint: {newIdolPoint}, Exceeds: {newIdolPoint >= idol.IdolReward.VoicePoint}");
                    updated = true;
                    break;
                }
            }

            if (updated) break;
        }

        // 更新されたデータをJSONファイルに書き戻す
        if (updated)
        {
            var path = Path.Combine(Application.streamingAssetsPath, "master_data/favorite_idol_data.json");
            var options = new JsonSerializerOptions
                { WriteIndented = true, Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };
            string updatedJson = JsonSerializer.Serialize(idolGroups, options);
            await File.WriteAllTextAsync(path, updatedJson, ct);
        }



        // リザルトのSABC別の必要ポイントと比較
        if (GlobalState.IdolResultRankPoint != null)
        {
            logger.ZLogTrace($"{GlobalState.IdolResultRankPoint}");
            for (int i = 0; i < GlobalState.IdolResultRankPoint.Count; i++)
            {
                float point = GlobalState.IdolResultRankPoint[i];

                if (kariInGameResult < point) continue;
                Index = i;
                logger.ZLogTrace($"{point}");
                break;
            }
        }

        string resultText = GlobalState.IdolResultRankText?[Index] ?? "Unknown text";
        string resultImage = resultImagePath[Index];

        logger.ZLogTrace($"Text: {resultText}, ResultImage:{resultImage}");

        view.IdolSupportDialogueTextElement.text = resultText;

        // 結果画像を表示
        var resultImageHandle = Addressables.LoadAssetAsync<Texture2D>(resultImage);
        await resultImageHandle.Task;
        if (resultImageHandle.Status == AsyncOperationStatus.Succeeded)
        {
            view.ResultPointImageVisualElement.style.backgroundImage =
                new StyleBackground(resultImageHandle.Result);
        }
        else
        {
            logger.ZLogError($"Failed to load result image: {resultImage}");
        }


        //手持ちポイントに追加する
        GlobalState.IdolRewardPoint += kariInGameResult;
        logger.ZLogTrace($"{GlobalState.IdolRewardPoint}");


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

    public async UniTask PlayVoice(CancellationToken ct)
    {
        string resultVoice = GlobalState.IdolResultRankVoice?[Index] ?? "Unknown voice";

        logger.ZLogTrace($"Voice: {resultVoice}");

        var voiceResult = new AssetReference(resultVoice);
        await audioPlayer.PlaySeAsync(voiceResult, 1.0f, ct);
    }


    public override void PreOpen()
    {
    }

    protected override void OnDispose()
    {
        bag.Dispose();
    }
}