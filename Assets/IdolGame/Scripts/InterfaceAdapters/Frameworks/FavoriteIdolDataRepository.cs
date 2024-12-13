using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;
using IdolGame.EnterpriseBusinessRules;

namespace IdolGame.Frameworks;

public class FavoriteIdolDataRepository : IAsyncRepository<IdolGroupData, IdolGroupId>
{
    private readonly IAsyncDataStore<IdolGroupData[]> dataStore;
    private IdolGroupData[]? data;

    public FavoriteIdolDataRepository(IAsyncDataStore<IdolGroupData[]> dataStore)
    {
        this.dataStore = dataStore;
    }

    public async UniTask<IEnumerable<IdolGroupData>> FindAllAsync(CancellationToken ct)
    {
        return (data ??= await dataStore.LoadAsync(ct))?.AsEnumerable() ?? Enumerable.Empty<IdolGroupData>();
    }

    public async UniTask<IdolGroupData?> FindAsync(IdolGroupId key, CancellationToken ct)
    {
        return (data ??= await dataStore.LoadAsync(ct))?.FirstOrDefault(x => x.GroupId == key);
    }

    public async UniTask StoreAsync(IdolGroupData? value, CancellationToken ct)
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