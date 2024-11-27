using System.Threading;
using Cysharp.Threading.Tasks;

namespace IdolGame.Frameworks;

/// <summary>
/// データストアの同期インターフェース
/// </summary>
/// <typeparam name="T">データの型</typeparam>
public interface IDataStore<T>
{
    /// <summary>
    /// データをロード
    /// </summary>
    /// <returns>ロードされたデータ</returns>
    T? Load();
    /// <summary>
    /// データを保存
    /// </summary>
    /// <param name="data">保存するデータ</param>
    void Store(T? data);
}
/// <summary>
/// データストアの非同期インターフェース
/// </summary>
/// <typeparam name="T">データの型</typeparam>
public interface IAsyncDataStore<T>
{
    /// <summary>
    /// データを非同期にロード
    /// </summary>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>ロードされたデータの非同期タスク</returns>
    UniTask<T?> LoadAsync(CancellationToken ct);
    /// <summary>
    /// データを非同期に保存
    /// </summary>
    /// <param name="data">保存するデータ</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>非同期タスク</returns>
    UniTask StoreAsync(T? data, CancellationToken ct);
}
