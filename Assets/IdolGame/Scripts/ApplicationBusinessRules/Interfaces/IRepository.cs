using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace IdolGame.ApplicationBusinessRules.Interfaces;

public interface IRepository<TValueObject, in TKey>
{
    IEnumerable<TValueObject> FindAll();
    TValueObject? Find(TKey key);
    void Store(TValueObject? value);
}
public interface IAsyncRepository<TValueObject, in TKey> where TValueObject : struct
{
    UniTask<IEnumerable<TValueObject>> FindAllAsync(CancellationToken ct);
    UniTask<TValueObject?> FindAsync(TKey key, CancellationToken ct);
    UniTask StoreAsync(TValueObject? value, CancellationToken ct);
}