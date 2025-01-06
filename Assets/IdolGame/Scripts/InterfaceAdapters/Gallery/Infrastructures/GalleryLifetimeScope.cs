using IdolGame.Common.infrastructures;
using IdolGame.Gallery.Presenter;
using IdolGame.Gallery.ViewModels;
using IdolGame.Scripts.InterfaceAdapters.Gallery.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace IdolGame.Gallery.Infrastructures
{


    public sealed class GalleryLifetimeScope : LifetimeScope
    {
        [SerializeField] UIDocument? rootDocument;
        [SerializeField] VisualTreeAsset? mainTreeAsset;
        [SerializeField] AssetReference? bgmAssetReference;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GalleryPresenter>();

            builder.Register<MainViewModel>(Lifetime.Scoped);
            builder.Register<MainView>(Lifetime.Scoped);

            // GlobalStateService を登録
            builder.Register<GlobalStateService>(Lifetime.Singleton); 
            builder.RegisterComponent(rootDocument);
            builder.RegisterInstance(mainTreeAsset);
            builder.RegisterInstance(bgmAssetReference);
        }

    }
}
