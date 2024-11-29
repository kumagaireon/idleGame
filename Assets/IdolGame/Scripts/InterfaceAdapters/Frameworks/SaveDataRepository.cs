using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;
using IdolGame.EnterpriseBusinessRules;

namespace IdolGame.Frameworks;

public sealed class SaveDataRepository : IAsyncRepository<SaveData,SaveDataId>
{
    private readonly IAsyncDataStore<SaveData[]> dataStore;
    private SaveData[]? data;

    public SaveDataRepository(IAsyncDataStore<SaveData[]> dataStore)
    {
        this.dataStore = dataStore;
    }

    public async UniTask<IEnumerable<SaveData>> FindAllAsync(CancellationToken ct)
    {
        return (data ??= await dataStore.LoadAsync(ct))?.AsEnumerable() ?? Enumerable.Empty<SaveData>();
    }

    public async UniTask<SaveData?> FindAsync(SaveDataId key, CancellationToken ct)
    {
        return (data ??= await dataStore.LoadAsync(ct))?.FirstOrDefault(x => x.Id == key);
    }

    public async UniTask StoreAsync(SaveData? value, CancellationToken ct)
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