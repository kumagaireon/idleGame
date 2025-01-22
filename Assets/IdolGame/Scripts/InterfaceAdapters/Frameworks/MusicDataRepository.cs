using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;

namespace IdolGame.Frameworks;

/// <summary>
/// 非同期リポジトリクラス: MusicDataのデータ操作を担当
/// </summary>
public sealed class MusicDataRepository : IAsyncRepository<MusicData, MusicId>
{
    private readonly IAsyncDataStore<MusicData[]> dataStore;// 非同期データストアのインスタンス
    private MusicData[]? data;// データをキャッシュするための配列

    public MusicDataRepository(IAsyncDataStore<MusicData[]> dataStore)
    {
        this.dataStore = dataStore;
    }

    public async UniTask<IEnumerable<MusicData>> FindAllAsync(CancellationToken ct)
    {
        return (data ??= await dataStore.LoadAsync(ct))?.AsEnumerable() ?? Enumerable.Empty<MusicData>();
    }

    public async UniTask<MusicData?> FindAsync(MusicId key, CancellationToken ct)
    {
        return (data ??= await dataStore.LoadAsync(ct))?.FirstOrDefault(x => Equals(x.Id, key));
    }

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