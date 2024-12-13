using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;
using IdolGame.EnterpriseBusinessRules;

namespace IdolGame.Frameworks;

public class FavoriteIdolDataRepository : IAsyncRepository<IdolGroup, IdolGroupId>
{
    private readonly IAsyncDataStore<IdolGroup[]> dataStore;
    private IdolGroup[]? data;

    public FavoriteIdolDataRepository(IAsyncDataStore<IdolGroup[]> dataStore)
    {
        this.dataStore = dataStore;
    }

    public async UniTask<IEnumerable<IdolGroup>> FindAllAsync(CancellationToken ct)
    {
        return (data ??= await dataStore.LoadAsync(ct))?.AsEnumerable() ?? Enumerable.Empty<IdolGroup>();
    }

    public async UniTask<IdolGroup?> FindAsync(IdolGroupId key, CancellationToken ct)
    {
        return (data ??= await dataStore.LoadAsync(ct))?.FirstOrDefault(x => x.Id == key);
    }

    public async UniTask StoreAsync(IdolGroup? value, CancellationToken ct)
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