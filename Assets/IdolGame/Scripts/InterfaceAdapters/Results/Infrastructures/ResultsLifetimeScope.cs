using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Results.Presenter;
using IdolGame.Results.ViewModels;
using IdolGame.Results.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace IdolGame.Results.Infrastructures;

public sealed class ResultsLifetimeScope:LifetimeScope
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
        builder.RegisterEntryPoint<ResultsPresenter>();
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
