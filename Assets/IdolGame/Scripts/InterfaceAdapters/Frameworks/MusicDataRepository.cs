using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;
using IdolGame.EnterpriseBusinessRules;
using IdolGame.Frameworks;

namespace IdolGame.InterfaceAdapters.Frameworks;

public sealed class MusicDataRepository:IAsyncRepository<MusicData,MusicId>
{
    private readonly IAsyncDataStore<MusicData[]> dataStore;
    private MusicData[]? data;

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
        
        data??= await dataStore.LoadAsync(ct);

        var currentLength = data?.Length ?? throw new NullReferenceException();
        Array.Resize(ref data, currentLength + 1);
        data[currentLength] = value.Value;

        await dataStore.StoreAsync(data, ct);
    }
}