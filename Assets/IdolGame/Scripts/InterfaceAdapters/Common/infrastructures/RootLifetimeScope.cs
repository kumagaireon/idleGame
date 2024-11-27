using System.IO;
using IdolGame.EnterpriseBusinessRules;
using IdolGame.Frameworks;
using Microsoft.Extensions.Logging;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ZLogger.Unity;

namespace IdolGame.Common.infrastructures;

/// <summary>
/// 依存性注入のライフタイムスコープを設定するクラス
/// </summary>
public class RootLifetimeScope:LifetimeScope
{
    [SerializeField] LogLevel minimumLevel;
    
    /// <summary>
    /// 依存性のコンテナを構成するメソッド
    /// </summary>
    /// <param name="builder">依存性注入のためのコンテナビルダー<</param>
    protected override void Configure(IContainerBuilder builder)
    {
        // ロガーファクトリのインスタンスを登録
        builder.RegisterInstance(LoggerFactory.Create(logger =>
        {
            logger.SetMinimumLevel(minimumLevel);// 最低ログレベルを設定
            logger.AddZLoggerUnityDebug();// ZLoggerのUnityデバッグ出力を追加
        }));
        // ILogger<>型のシングルトンインスタンスを登録
        builder.Register(typeof(Logger<>), Lifetime.Singleton).As(typeof(ILogger<>));
        // JsonAsyncDataStore<MusicData[]>型のシングルトンインスタンスを登録
        builder.Register<JsonAsyncDataStore<MusicData[]>>(Lifetime.Singleton)
            .WithParameter(Path.Combine(
                Application.streamingAssetsPath, "master_data", "music_data.json"))// ファイルパスをパラメータとして指定
            .As<IAsyncDataStore<MusicData[]>>();// インターフェースとして登録
    }
}