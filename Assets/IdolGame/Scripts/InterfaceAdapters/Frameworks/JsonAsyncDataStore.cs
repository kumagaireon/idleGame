using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using Cysharp.IO;
using Cysharp.Threading.Tasks;

namespace IdolGame.Frameworks;

/// <summary>
/// JSON形式の非同期データストアクラス
/// </summary>
/// <typeparam name="T">データの型</typeparam>
public sealed class JsonAsyncDataStore<T> : IAsyncDataStore<T>
{
    readonly string path;// データファイルのパス
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="path">データファイルのパス</param>
    public JsonAsyncDataStore(string path)
    {
        this.path = path;
    }
    /// <summary>
    /// 非同期にデータをロード
    /// </summary>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>ロードされたデータの非同期タスク</returns>
    public async UniTask<T?> LoadAsync(CancellationToken ct)
    {
        byte[]? bytes;
        await using (UniTask.ReturnToMainThread())// メインスレッドに戻る
        {
            await UniTask.SwitchToTaskPool();// タスクプールに切り替え
            await using var stream = new FileStream(path, FileMode.Open);
            await using var reader = new Utf8StreamReader(stream);
            bytes = await reader.ReadToEndAsync(ct);
        }

        return JsonSerializer.Deserialize<T>(bytes.AsSpan());// JSONデータをデシリアライズ
    }

    /// <summary>
    /// 非同期にデータを保存
    /// </summary>
    /// <param name="data">保存するデータ</param>
    /// <param name="ct">キャンセルトークン</param>
    public async UniTask StoreAsync(T? data, CancellationToken ct)
    {
        await using (UniTask.ReturnToMainThread())// メインスレッドに戻る
        {
            await UniTask.SwitchToTaskPool();// タスクプールに切り替え
            await using var stream = new FileStream(path, FileMode.Create);
            var bytes = JsonSerializer.SerializeToUtf8Bytes(data);// データをJSON形式にシリアライズ

            await stream.WriteAsync(bytes, ct);// 非同期にファイルに書き込み
            await stream.FlushAsync(ct);// 非同期にバッファをフラッシュ
        }
    }
}