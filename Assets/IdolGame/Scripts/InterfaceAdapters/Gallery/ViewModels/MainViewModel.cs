using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.Audios.Core;
using IdolGame.Common.infrastructures;
using IdolGame.Common.ViewModels;
using IdolGame.Recommendation;
using IdolGame.Scripts.InterfaceAdapters.Gallery.Views;
using IdolGame.UIElements;
using Microsoft.Extensions.Logging;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using ZLogger;

namespace IdolGame.Gallery.ViewModels;

public sealed class MainViewModel : ViewModelBase<MainView>
{
    readonly ILogger<MainViewModel> logger;
    DisposableBag bag;
    readonly AudioPlayer audioPlayer;
    readonly AssetReference bgmAssetReference;
    
    public MainViewModel(ILogger<MainViewModel> logger,
        MainView view,
        UIDocument rootDocument, 
        AudioPlayer audioPlayer,
        AssetReference bgmAssetReference)
        : base(view, rootDocument,
            new FadeViewTransition(rootDocument))
    {
        this.logger = logger;
        this.audioPlayer = audioPlayer;
        this.bgmAssetReference = bgmAssetReference;
    }

    /// <summary>
    /// 非同期にビューを初期化するメソッド
    /// </summary>
    /// <param name="ct">キャンセルトークン</param>
    public async UniTask InitializeAsync(CancellationToken ct)
    {
        logger.ZLogTrace($"Called {GetType().Name}.InitializeAsync");
        
        var idolDataLoader = new IdolDataLoader();
        var idolGroups = await idolDataLoader.LoadIdolDataAsync();
        foreach (var group in idolGroups)
        {
            if (group.Members == null) continue;
            for (int i = 0; i < group.Members.Length; i++)
            {
                var idol = group.Members[i];
                if (idol.Id == GlobalState.IdolId)
                {
                    var cheki1Points = idol.IdolReward.Cheki1Point - idol.IdolReward.IdolPoint;
                    var cheki2Points = idol.IdolReward.Cheki2Point - idol.IdolReward.IdolPoint;
                    var cheki3Points = idol.IdolReward.Cheki3Point - idol.IdolReward.IdolPoint;
                    var voicePoints = idol.IdolReward.VoicePoint - idol.IdolReward.IdolPoint;

                    if (cheki1Points > 0)
                    {
                        view.IdleChekiReleasePoint1TextElement.text = $"残り{cheki1Points}ポイントで解放！";
                        view.IdolChekiDate1TextElement.style.display = DisplayStyle.None;
                    }
                    else
                    {
                        view.IdleChekiReleasePoint1TextElement.style.display = DisplayStyle.None;
                        
                        view.IdolChekiDate1TextElement.text =
                            idol.IdolReward.DateAcquisitioRewardCheck1.ToString("yyyy/MM/dd");
                        var chekiImageHandle =
                            Addressables.LoadAssetAsync<Texture2D>(idol.IdolReward.RewardChekiImage1Path.ToString());
                        await chekiImageHandle.Task;
                        if (chekiImageHandle.Status == AsyncOperationStatus.Succeeded)
                        {
                            view.IdolPic1VisualElementElement.style.backgroundImage =
                                new StyleBackground(chekiImageHandle.Result);
                        }
                        else
                        {
                            logger.ZLogError($"Failed to load cheki1 image: {idol.IdolReward.RewardChekiImage1Path}");
                        }
                    }
                    
                    if (cheki2Points > 0)
                    {
                        view.IdleChekiReleasePoint2TextElement.text = $"残り{cheki2Points}ポイントで解放！";
                        view.IdolChekiDate2TextElement.style.display = DisplayStyle.None;
                    }
                    else
                    {
                        view.IdleChekiReleasePoint2TextElement.style.display = DisplayStyle.None;
                        
                        view.IdolChekiDate2TextElement.text =
                            idol.IdolReward.DateAcquisitioRewardCheck2.ToString("yyyy/MM/dd");
                        var chekiImageHandle =
                            Addressables.LoadAssetAsync<Texture2D>(idol.IdolReward.RewardChekiImage2Path.ToString());
                        await chekiImageHandle.Task;
                        if (chekiImageHandle.Status == AsyncOperationStatus.Succeeded)
                        {
                            view.IdolPic2VisualElementElement.style.backgroundImage =
                                new StyleBackground(chekiImageHandle.Result);
                        }
                        else
                        {
                            logger.ZLogError($"Failed to load cheki2 image: {idol.IdolReward.RewardChekiImage2Path}");
                        }
                    }
                    
                    if (cheki3Points > 0)
                    {
                        view.IdleChekiReleasePoint3TextElement.text = $"残り{cheki3Points}ポイントで解放！";
                        view.IdolChekiDate3TextElement.style.display = DisplayStyle.None;
                    }
                    else
                    {
                        view.IdleChekiReleasePoint3TextElement.style.display = DisplayStyle.None;
                        
                        view.IdolChekiDate3TextElement.text =
                            idol.IdolReward.DateAcquisitioRewardCheck3.ToString("yyyy/MM/dd");
                        var chekiImageHandle =
                            Addressables.LoadAssetAsync<Texture2D>(idol.IdolReward.RewardChekiImage3Path.ToString());
                        await chekiImageHandle.Task;
                        if (chekiImageHandle.Status == AsyncOperationStatus.Succeeded)
                        {
                            view.IdolPic3VisualElementElement.style.backgroundImage =
                                new StyleBackground(chekiImageHandle.Result);
                        }
                        else
                        {
                            logger.ZLogError($"Failed to load cheki3 image: {idol.IdolReward.RewardChekiImage3Path}");
                        }
                    }

                 
         
                    view.IdolVideoReleasePointTextElement.text = voicePoints > 0
                        ? $"残り{voicePoints}ポイントで解放！"
                        : $"解放済み (日付: {idol.IdolReward.DateAcquisitioRewardVideo:yyyy/MM/dd})";

                 
         
                    view.IdolVideoDateTextElement.text =
                        idol.IdolReward.DateAcquisitioRewardVideo.ToString("yyyy/MM/dd");
         
                    view.AccumulatedPointTextElement.text = $"累計ポイント: {idol.IdolReward.IdolPoint}";
                }

            }
        }

        
        view.ReturnVisualElement.OnInputAsObservable()
            .SubscribeAwait(async (e, ct2)
                => await OnInputReturn(e, ct2))
            .AddTo(ref bag);
        
        
        
        // 非同期処理のためにフレームを待機
        await UniTask.Yield(ct);
    }

    private IdolMembersData? GetSelectedIdol(List<IdolGroupData> idolGroups, int? idolId)
    {
        foreach (var group in idolGroups)
        {
            if (group.Members == null) continue;
            foreach (var idol in group.Members)
            {
                if (idol.Id == idolId)
                {
                    return idol;
                }
            }
        }

        return null;
    }

    async UniTask OnInputReturn(PointerDownEvent e, CancellationToken ct)
    {
        logger.ZLogInformation($"推し設定画面に戻る");
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
    }
}