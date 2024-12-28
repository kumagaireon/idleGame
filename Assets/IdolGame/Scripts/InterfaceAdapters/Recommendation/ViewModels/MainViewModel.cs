using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.Audios.Core;
using IdolGame.Common.infrastructures;
using IdolGame.Common.ViewModels;
using IdolGame.Recommendation.Views;
using IdolGame.UIElements;
using Microsoft.Extensions.Logging;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;
using ZLogger;

namespace IdolGame.Recommendation.ViewModels;

public sealed class MainViewModel : ViewModelBase<MainView>
{
    readonly ILogger<MainViewModel> logger;
    readonly IAudioPlayerService audioPlayerService;
    List<IdolMembersData>? members; 
    DisposableBag bag;


    public MainViewModel(ILogger<MainViewModel> logger,
        MainView view, UIDocument rootDocument,
        IAudioPlayerService audioPlayerService) : base(
        view, rootDocument, new FadeViewTransition(rootDocument))
    {
        this.logger = logger;
        this.audioPlayerService = audioPlayerService;
    }


    public async UniTask InitializeAsync(CancellationToken ct)
    {
        logger.ZLogTrace($"Called {GetType().Name}.InitializeAsync");
        var scrollView = view.IdolScrollView as CustomScrollView;
        
        var idolDataLoader = new IdolDataLoader();
        var idolGroups = await idolDataLoader.LoadIdolDataAsync();

        foreach (var group in idolGroups)
        {
            foreach (var idol in group.Members)
            {
                var element = scrollView.itemsTemplate.Instantiate();
                element.Q<Label>("idol-name").text = idol.Name.ToString();
                var handle = Addressables.LoadAssetAsync<Texture2D>(idol.ImagePath.ToString());
                await handle.Task;
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    var imageElement = element.Q<VisualElement>("idol-image");
                    imageElement.style.backgroundImage = new StyleBackground(handle.Result);
                }

                element.OnInputAsObservable()
                    .SubscribeAwait(async (evt, ct2) =>
                        await OnInputSelfIntroduction(evt, ct2, idol,group))
                    .AddTo(ref bag);


                scrollView.Add(element);
            }
        }

        view.GalleryVisualElement.OnInputAsObservable()
            .SubscribeAwait(async (e, ct2)
                => await OnInputGallery(e, ct2))
            .AddTo(ref bag);

        view.ReturnVisualElement.OnInputAsObservable()
            .SubscribeAwait(async (e, ct2)
                => await OnInputReturn(e, ct2))
            .AddTo(ref bag);

        await UniTask.Yield(ct);
    }


    async UniTask OnInputSelfIntroduction(
        PointerDownEvent e, CancellationToken ct, IdolMembersData idol, IdolGroupData idolGroup)
    {
        view.ExplanatoryTextElementext.text = idol.IdolSelfIntroduction.SelfIntroductionText;

        var voiceReference = new AssetReference(idol.IdolSelfIntroduction.SelfIntroductionVoicePath);
        await audioPlayerService.PlaySeAsync(voiceReference, 1.0f, ct);

       
        GlobalState.GroupId = idolGroup.GroupId;
        GlobalState.GroupBackgroundImagePath = idolGroup.ImagePath;
        GlobalState.GroupButtonUIPath = idolGroup.IdolButtonUIPath;
        
        GlobalState.IdolId = idol.Id;
        GlobalState.IdolImagePath = idol.ImagePath.ToString();
        GlobalState.IdolColor = idol.CollarCode.ToString();
        GlobalState.IdolSerifMenuText = idol.SerifMenuText;
        GlobalState.IdolRewardPoint = idol.IdolReward.IdolPoint;

        GlobalState.IdolResultRankPoint = new List<float>
        {
            idol.ResultIdol.SRankPoint,
            idol.ResultIdol.ARankPoint,
            idol.ResultIdol.BRankPoint,
            idol.ResultIdol.CRankPoint
        };
        GlobalState.IdolResultRankVoice = new List<string>
        {
            idol.ResultIdol.SRankVoice,
            idol.ResultIdol.ARankVoice,
            idol.ResultIdol.BRankVoice,
            idol.ResultIdol.CRankVoice
        };
        GlobalState.IdolResultRankText = new List<string>
        {
            idol.ResultIdol.SRank.ToString(),
            idol.ResultIdol.ARank.ToString(),
            idol.ResultIdol.BRank.ToString(),
            idol.ResultIdol.CRank.ToString()
        };
        
        
        await UniTask.Yield();
    }

    async UniTask OnInputGallery(PointerDownEvent e, CancellationToken ct)
    {
        logger.ZLogInformation($"ギャラリー画面遷移");
    }

    async UniTask OnInputReturn(PointerDownEvent e, CancellationToken ct)
    {
        logger.ZLogInformation($"メニュー画面に戻る");
    }
    
    protected override void PreOpen()
    {
    }

    protected override void OnDispose()
    {
        bag.Dispose();
    }
}