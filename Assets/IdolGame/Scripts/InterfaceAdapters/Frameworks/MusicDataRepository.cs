using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;
using IdolGame.EnterpriseBusinessRules;
using IdolGame.Frameworks;

namespace IdolGame.InterfaceAdapters.Frameworks;

/// <summary>
/// 非同期リポジトリクラス: MusicDataのデータ操作を担当
/// </summary>
public sealed class MusicDataRepository : IAsyncRepository<MusicData, MusicId>
{
    private readonly IAsyncDataStore<MusicData[]> dataStore;// 非同期データストアのインスタンス
    private MusicData[]? data;// データをキャッシュするための配列

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="dataStore">非同期データストアのインスタンス</param>
    public MusicDataRepository(IAsyncDataStore<MusicData[]> dataStore)
    {
        this.dataStore = dataStore;
    }

    /// <summary>
    /// 非同期に全データを取得
    /// </summary>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>全データの非同期タスク</returns>
    public async UniTask<IEnumerable<MusicData>> FindAllAsync(CancellationToken ct)
    {
        return (data ??= await dataStore.LoadAsync(ct))?.AsEnumerable() ?? Enumerable.Empty<MusicData>();
    }

    /// <summary>
    /// 非同期に指定されたIDのデータを取得
    /// </summary>
    /// <param name="key">データのキー</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>指定されたIDのデータの非同期タスク</returns>
    public async UniTask<MusicData?> FindAsync(MusicId key, CancellationToken ct)
    {
        return (data ??= await dataStore.LoadAsync(ct))?.FirstOrDefault(x => Equals(x.Id, key));
    }

    /// <summary>
    /// 非同期にデータを保存
    /// </summary>
    /// <param name="value">保存するデータ</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <exception cref="NullReferenceException">データがnullの場合の例外</exception>
    public async UniTask StoreAsync(MusicData? value, CancellationToken ct)
    {
        if (value is null)
        {
            return;
        }

        data ??= await dataStore.LoadAsync(ct);

        var currentLength = data?.Length ?? throw new NullReferenceException();
        Array.Resize(ref data, currentLength + 1);
        data[currentLength] = value.Value;

        await dataStore.StoreAsync(data, ct);
    }
}