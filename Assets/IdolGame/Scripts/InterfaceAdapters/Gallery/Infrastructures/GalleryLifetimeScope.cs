

using IdolGame.ApplicationBusinessRules.UseCases;
using IdolGame.Gallery.Presenter;
using IdolGame.Gallery.ViewModels;
using IdolGame.Scripts.InterfaceAdapters.Gallery.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace IdolGame.Gallery.Infrastructures;

public sealed class GalleryLifetimeScope: LifetimeScope
{
    [SerializeField] UIDocument? rootDocument;
    [SerializeField] VisualTreeAsset? mainTreeAsset;
    [SerializeField] AssetReference? bgmAssetReference;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GalleryPresenter>();
        
        builder.Register<MainViewModel>(Lifetime.Scoped);
        builder.Register<MainView>(Lifetime.Scoped);

        builder.Register<FindSaveDataUseCase>(Lifetime.Scoped);
            
        builder.RegisterComponent(rootDocument);
        builder.RegisterInstance(mainTreeAsset);
        builder.RegisterInstance(bgmAssetReference);
    }
    
}