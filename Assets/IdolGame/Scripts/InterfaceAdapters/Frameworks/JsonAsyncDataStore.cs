using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using Cysharp.IO;
using Cysharp.Threading.Tasks;

namespace IdolGame.Frameworks;

public sealed class JsonAsyncDataStore<T> : IAsyncDataStore<T>
{
    readonly string path;
    
    public JsonAsyncDataStore(string path)
    {
        this.path = path;
    }
    
    public async UniTask<T?> LoadAsync(CancellationToken ct)
    {
        byte[]? bytes;
        await using (UniTask.ReturnToMainThread())
        {
            await UniTask.SwitchToTaskPool();
            await using var stream = new FileStream(path, FileMode.Open);
            await using var reader = new Utf8StreamReader(stream);
            bytes = await reader.ReadToEndAsync(ct);
        }

        return JsonSerializer.Deserialize<T>(bytes.AsSpan());
    }

    public async UniTask StoreAsync(T? data, CancellationToken ct)
    {
        await using (UniTask.ReturnToMainThread())
        {
            await UniTask.SwitchToTaskPool();
            await using var stream = new FileStream(path, FileMode.Create);
            var bytes = JsonSerializer.SerializeToUtf8Bytes(data);

            await stream.WriteAsync(bytes, ct);
            await stream.FlushAsync(ct);
        }
    }
}