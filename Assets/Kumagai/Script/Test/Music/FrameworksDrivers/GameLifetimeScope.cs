using UnityEngine;
using VContainer;
using VContainer.Unity;
using Kumagai.InterfaceAdapters;
using Kumagai.UseCase;

namespace Kumagai.FrameworksDrivers
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameObject csvLoaderPrefab; // CSVLoaderAdapter プレハブ
        [SerializeField] private GameObject[] iconPrefabs;
        [SerializeField] private CSVMana csvMana;

        protected override void Configure(IContainerBuilder builder)
        {
           // Debug.Log("GameLifetimeScope Configure method called");
            if (csvLoaderPrefab != null)
            {
               // Debug.Log("Instantiating CSVLoaderAdapter from prefab...");
                var csvLoaderAdapter = Instantiate(csvLoaderPrefab).GetComponent<CSVLoaderAdapter>();
                if (csvLoaderAdapter != null)
                {
                   // Debug.Log("CSVLoaderAdapter instantiated and registered successfully.");
                }
                else
                {
                    Debug.LogError("CSVLoaderAdapter instantiation failed.");
                }

                builder.RegisterInstance(csvLoaderAdapter).AsSelf().AsImplementedInterfaces();
                // CSVMana に csvLoaderAdapter を設定
                csvMana.Initialize(csvLoaderAdapter);
            }
            else
            {
                Debug.LogError("CSVLoaderPrefab is null. Please check the inspector settings.");
            }

            builder.RegisterInstance(iconPrefabs);
            builder.Register<IMusicDataLoader, MusicDataLoader>(Lifetime.Singleton);
            builder.Register<IIconManager, IconManager>(Lifetime.Singleton).WithParameter("iconPrefabs", iconPrefabs);
        }
    }
}