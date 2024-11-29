using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Titles.Presenter;
using IdolGame.Titles.ViewModels;
using IdolGame.Titles.Views;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace IdolGame.Titles.Infrastructures
{
    public sealed class TitleLifetimeScope : LifetimeScope
    {
        [SerializeField] UIDocument? rootDocument;
        [SerializeField] VisualTreeAsset? mainTreeAsset;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TitlePresenter>();
        
            builder.Register<MainViewModel>(Lifetime.Scoped);
            builder.Register<MainView>(Lifetime.Scoped);

            builder.Register<FindSaveDataUseCase>(Lifetime.Scoped);
            
            builder.RegisterComponent(rootDocument);
            builder.RegisterInstance(mainTreeAsset);
        }
    }
    /*/// <summary>
    /// タイトル画面のライフタイムスコープクラス
    /// </summary>
    public sealed class TitleLifetimeScope : LifetimeScope
    {
        // UIドキュメントをシリアライズフィールドとして持つ
        [SerializeField] UIDocument? rootDocument;

        // ビジュアルツリーアセットをシリアライズフィールドとして持つ
        [SerializeField] VisualTreeAsset? mainTreeAsset;

        /// <summary>
        /// コンテナビルダーの設定を行うメソッド
        /// </summary>
        /// <param name="builder">コンテナビルダー</param>
        protected override void Configure(IContainerBuilder builder)
        {
            // TitlePresenterをエントリーポイントとして登録
            builder.RegisterEntryPoint<TitlePresenter>();
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
        }
    }*/
}