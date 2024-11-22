using System.Threading;
using Cysharp.Threading.Tasks;

namespace IdolGame.Frameworks;

public interface IDataStore<T>
{
    T? Load();
    void Store(T? data);
}
public interface IAsyncDataStore<T>
{
    UniTask<T?> LoadAsync(CancellationToken ct);
    UniTask StoreAsync(T? data, CancellationToken ct);
}
