using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Common.infrastructures;
using IdolGame.Titles.Presenter;
using IdolGame.Titles.ViewModels;
using IdolGame.Titles.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace IdolGame.Titles.Infrastructures
{
    // タイトル画面のライフタイムスコープクラス
    public sealed class TitleLifetimeScope : LifetimeScope
    {
        // UIドキュメントをシリアライズフィールドとして持つ
        [SerializeField] UIDocument? rootDocument;

        // ビジュアルツリーアセットをシリアライズフィールドとして持つ
        [SerializeField] VisualTreeAsset? mainTreeAsset;

        [SerializeField] AssetReference? bgmAssetReference;
       
        // コンテナビルダーの設定を行うメソッド
        protected override void Configure(IContainerBuilder builder)
        {
            // TitlePresenterをエントリーポイントとして登録
            builder.RegisterEntryPoint<TitlePresenter>();
            // メインビューモデルをスコープライフタイムで登録
            builder.Register<MainViewModel>(Lifetime.Scoped);
            // メインビューをスコープライフタイムで登録
            builder.Register<MainView>(Lifetime.Scoped);
            
            // GlobalStateService を登録
            builder.Register<GlobalStateService>(Lifetime.Singleton); 
            
            // シリアライズフィールドのUIドキュメントを登録
            builder.RegisterComponent(rootDocument);
            // シリアライズフィールドのビジュアルツリーアセットをインスタンスとして登録
            builder.RegisterInstance(mainTreeAsset);
            builder.RegisterInstance(bgmAssetReference);
        }
    }
}