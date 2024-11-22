using System.IO;
using IdolGame.EnterpriseBusinessRules;
using IdolGame.Frameworks;
using Microsoft.Extensions.Logging;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ZLogger.Unity;

namespace IdolGame.Common.infrastructures;

public class RootLifetimeScope:LifetimeScope
{
    [SerializeField] LogLevel minimumLevel;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(LoggerFactory.Create(logger =>
        {
            logger.SetMinimumLevel(minimumLevel);
            logger.AddZLoggerUnityDebug();
        }));
        
        builder.Register(typeof(Logger<>), Lifetime.Singleton).As(typeof(ILogger<>));
        
        builder.Register<JsonAsyncDataStore<MusicData[]>>(Lifetime.Singleton)
            .WithParameter(Path.Combine(
                Application.streamingAssetsPath, "master_data", "music_data.json"))
            .As<IAsyncDataStore<MusicData[]>>();
    }
}