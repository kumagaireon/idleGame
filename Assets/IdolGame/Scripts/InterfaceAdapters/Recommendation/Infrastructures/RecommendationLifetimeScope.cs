using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Recommendation.Presenter;
using IdolGame.Recommendation.ViewModels;
using IdolGame.Recommendation.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace IdolGame.Recommendation.Infrastructures
{

    public sealed class RecommendationLifetimeScope : LifetimeScope
    {
        [SerializeField] UIDocument? rootDocument;
        [SerializeField] VisualTreeAsset? mainTreeAsset;
        [SerializeField] AssetReference? bgmAssetReference;


        protected override void Configure(IContainerBuilder builder)
        {
            // Presenter をエントリーポイントとして登録
            builder.RegisterEntryPoint<RecommendationPresenter>();
            // メインビューモデルとビューをスコープライフタイムで登録
            builder.Register<MainViewModel>(Lifetime.Scoped);
            builder.Register<MainView>(Lifetime.Scoped);
            // UIドキュメントとビジュアルツリーアセットを登録
            builder.RegisterComponent(rootDocument);
            builder.RegisterInstance(mainTreeAsset);
            // BGMアセットリファレンスを登録
            builder.RegisterInstance(bgmAssetReference);
        }
    }
}
