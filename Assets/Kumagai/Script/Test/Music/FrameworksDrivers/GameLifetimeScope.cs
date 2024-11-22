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
        [SerializeField] private CSVMana csvMana; // CSVManaのインスタンス

        /// <summary>
        /// Configureメソッドは、依存性注入の設定を行うためにオーバーライドされる
        /// </summary>
        /// <param name="builder"></param>
        protected override void Configure(IContainerBuilder builder)
        {
            // Debug.Log("GameLifetimeScope Configure method called");
            if (csvLoaderPrefab != null)
            {
                // CSVLoaderAdapterプレハブをインスタンス化し、そのコンポーネントを取得
                // Debug.Log("Instantiating CSVLoaderAdapter from prefab...");
                var csvLoaderAdapter = Instantiate(csvLoaderPrefab).GetComponent<CSVLoaderAdapter>();
                if (csvLoaderAdapter != null)
                {
                    // CSVLoaderAdapterのインスタンス化と登録が成功したことをログに出力
                    // Debug.Log("CSVLoaderAdapter instantiated and registered successfully.");
                }
                else
                {
                    // CSVLoaderAdapterのインスタンス化に失敗した場合のエラーログ
                    Debug.LogError("CSVLoaderAdapter instantiation failed.");
                }

// CSVLoaderAdapterのインスタンスをDIコンテナに登録
                builder.RegisterInstance(csvLoaderAdapter).AsSelf().AsImplementedInterfaces();
                // CSVMana に csvLoaderAdapter を設定
                csvMana.Initialize(csvLoaderAdapter);
            }
            else
            {
                // CSVLoaderPrefabが設定されていない場合のエラーログ
                Debug.LogError("CSVLoaderPrefab is null. Please check the inspector settings.");
            }

            // iconPrefabsのインスタンスをDIコンテナに登録
            builder.RegisterInstance(iconPrefabs);
            // MusicDataLoaderインスタンスをシングルトンとしてDIコンテナに登録
            builder.Register<IMusicDataLoader, MusicDataLoader>(Lifetime.Singleton);
            // IconManagerインスタンスをシングルトンとして、iconPrefabsをパラメータとしてDIコンテナに登録
            builder.Register<IIconManager, IconManager>(Lifetime.Singleton).WithParameter("iconPrefabs", iconPrefabs);
        }
    }
}