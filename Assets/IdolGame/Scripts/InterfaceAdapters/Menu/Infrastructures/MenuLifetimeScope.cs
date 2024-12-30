using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Common.infrastructures;
using IdolGame.Menu.Presenter;
using IdolGame.Menu.ViewModels;
using IdolGame.Menu.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace IdolGame.Menu.Infrastructures
{
    public sealed class MenuLifetimeScope : LifetimeScope
    {
        // UIドキュメントをシリアライズフィールドとして持つ
        [SerializeField] UIDocument? rootDocument;
        // ビジュアルツリーアセットをシリアライズフィールドとして持つ
        [SerializeField] VisualTreeAsset? mainTreeAsset;
        // BGMのアセットリファレンス
        [SerializeField] AssetReference? bgmAssetReference;
      
        protected override void Configure(IContainerBuilder builder)
        {
            // TitlePresenterをエントリーポイントとして登録
            builder.RegisterEntryPoint<MenuPresenter>();
            // メインビューモデルをスコープライフタイムで登録
            builder.Register<MainViewModel>(Lifetime.Scoped); 
            // メインビューをスコープライフタイムで登録
            builder.Register<MainView>(Lifetime.Scoped).WithParameter(mainTreeAsset); 
            // セーブデータ取得ユースケースをスコープライフタイムで登録
            builder.Register<FindSaveDataUseCase>(Lifetime.Scoped);
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
