using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace IdolGame.ApplicationBusinessRules.Interfaces;

/// <summary>
/// 同期データリポジトリのインターフェース
/// </summary>
/// <typeparam name="TValueObject">>データの型</typeparam>
/// <typeparam name="TKey">データのキーの型</typeparam>
public interface IRepository<TValueObject, in TKey>
{
    /// <summary>
    /// 全データを取得
    /// </summary>
    /// <returns>データの列挙</returns>
    IEnumerable<TValueObject> FindAll();
    /// <summary>
    /// 指定されたキーのデータを取得
    /// </summary>
    /// <param name="key">データのキー</param>
    /// <returns>指定されたキーのデータ</returns>
    TValueObject? Find(TKey key);
    /// <summary>
    /// データを保存
    /// </summary>
    /// <param name="value">保存するデータ</param>
    void Store(TValueObject? value);
}
/// <summary>
/// 非同期データリポジトリのインターフェース
/// </summary>
/// <typeparam name="TValueObject">データの型</typeparam>
/// <typeparam name="TKey">データのキーの型</typeparam>
public interface IAsyncRepository<TValueObject, in TKey> where TValueObject : struct
{
    /// <summary>
    /// 非同期に全データを取得
    /// </summary>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>全データの非同期タスク</returns>
    UniTask<IEnumerable<TValueObject>> FindAllAsync(CancellationToken ct);
    /// <summary>
    /// 非同期に指定されたキーのデータを取得
    /// </summary>
    /// <param name="key">保存するデータ</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>指定されたキーのデータの非同期タスク</returns>
    UniTask<TValueObject?> FindAsync(TKey key, CancellationToken ct);
    /// <summary>
    /// 非同期にデータを保存
    /// </summary>
    /// <param name="value">保存するデータ</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>非同期タスク</returns>
    UniTask StoreAsync(TValueObject? value, CancellationToken ct);
}