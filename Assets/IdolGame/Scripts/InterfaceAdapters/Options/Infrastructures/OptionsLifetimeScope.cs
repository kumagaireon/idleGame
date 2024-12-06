using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Options.Presenter;
using IdolGame.Options.ViewModels;
using IdolGame.Options.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace IdolGame.Options.Infrastructures
{


    public sealed class OptionsLifetimeScope : LifetimeScope
    {
        // UIドキュメントをシリアライズフィールドとして持つ
        [SerializeField] UIDocument? rootDocument;

        // ビジュアルツリーアセットをシリアライズフィールドとして持つ
        [SerializeField] VisualTreeAsset? mainTreeAsset;

        [SerializeField] AssetReference? bgmAssetReference;

        /// <summary>
        /// コンテナビルダーの設定を行うメソッド
        /// </summary>
        /// <param name="builder">コンテナビルダー</param>
        protected override void Configure(IContainerBuilder builder)
        {
            // TitlePresenterをエントリーポイントとして登録
            builder.RegisterEntryPoint<OptionsPresenter>();
            // メインビューモデルをスコープライフタイムで登録
            builder.Register<MainViewModel>(Lifetime.Scoped);
            // メインビューをスコープライフタイムで登録
            builder.Register<MainView>(Lifetime.Scoped);

            // セーブデータ取得ユースケースをスコープライフタイムで登録
            builder.Register<FindSaveDataUseCase>(Lifetime.Scoped);
            // シリアライズフィールドのUIドキュメントを登録
            builder.RegisterComponent(rootDocument);
            // シリアライズフィールドのビジュアルツリーアセットをインスタンスとして登録
            builder.RegisterInstance(mainTreeAsset);
            builder.RegisterInstance(bgmAssetReference);
        }
    }
}
