using System.IO;
using IdolGame.ApplicationBusinessRules.Interfaces;
using IdolGame.Audios.Core;
using IdolGame.Audios.Infrastructures;
using IdolGame.Frameworks;
using Microsoft.Extensions.Logging;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ZLogger.Unity;

namespace IdolGame.Common.infrastructures
{
    /// <summary>
    /// 依存性注入のライフタイムスコープを設定するクラス
    /// </summary>
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] LogLevel minimumLevel;
        [SerializeField] AudioPlayerSettings audioPlayerSettings;
        [SerializeField] GameObject? audioContainer;
        
        /// <summary>
        /// 依存性のコンテナを構成するメソッド
        /// </summary>
        /// <param name="builder">依存性注入のためのコンテナビルダー<</param>
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(LoggerFactory.Create(logger =>
            {
                logger.SetMinimumLevel(minimumLevel);
                logger.AddZLoggerUnityDebug();
            }));

            builder.Register(typeof(Logger<>), Lifetime.Singleton).As(typeof(ILogger<>));

            builder.Register<JsonAsyncDataStore<SaveData[]>>(Lifetime.Singleton)
                .WithParameter(Path.Combine(
                    Application.streamingAssetsPath, "master_data", "save_data.json"))
                .As<IAsyncDataStore<SaveData[]>>();

            builder.Register<SaveDataRepository>(Lifetime.Singleton)
                .As<IAsyncRepository<SaveData, SaveDataId>>();
        
           
            /*
            // JsonAsyncDataStore<MusicData[]>型のシングルトンインスタンスを登録
            builder.Register<JsonAsyncDataStore<MusicData[]>>(Lifetime.Singleton)
                .WithParameter(Path.Combine(
                    Application.streamingAssetsPath, "master_data", "music_data.json")) // ファイルパスをパラメータとして指定
                .As<IAsyncDataStore<MusicData[]>>(); // インターフェースとして登録
            
            builder.Register<SaveDataRepository>(Lifetime.Singleton)
                .As<IAsyncRepository<MusicData, MusicId>>();
                */

            builder.Register<AudioPlayer>(Lifetime.Singleton);
            builder.Register<AddressableAudioLoader>(Lifetime.Singleton).As<IAudioLoader>();
            builder.Register<AudioPlayerServiceImpl>(Lifetime.Singleton)
                .WithParameter(audioPlayerSettings)
                .WithParameter(audioContainer).As<IAudioPlayerService>();
        }
    }
}
