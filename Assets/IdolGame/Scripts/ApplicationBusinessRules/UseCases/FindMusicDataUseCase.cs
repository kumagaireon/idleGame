using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;

namespace IdolGame.ApplicationBusinessRules.UseCases;

/// <summary>
/// 音楽データを検索するユースケースクラス
/// </summary>
public class FindMusicDataUseCase
{
    // 非同期リポジトリのインスタンス
    private readonly IAsyncRepository<MusicData, MusicId> repository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="repository">非同期リポジトリのインスタンス</param>
    public FindMusicDataUseCase(IAsyncRepository<MusicData, MusicId> repository)
    {
        this.repository =  repository;
    }
    /// <summary>
    /// 全ての音楽データを非同期に取得
    /// </summary>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>全ての音楽データの非同期タスク</returns>
    public async UniTask<IEnumerable<MusicData>> FindAllAsync(CancellationToken ct)
    {
        return  await repository.FindAllAsync(ct);
    }

    /// <summary>
    /// 指定されたIDの音楽データを非同期に取得
    /// </summary>
    /// <param name="id">音楽データのID</param>
    /// <param name="ct">キャンセルトークン</param>
    /// <returns>指定されたIDの音楽データの非同期タスク</returns>
    public async UniTask<MusicData?> FindByAsync(MusicId id, CancellationToken ct)
    {
        return await repository.FindAsync(id, ct);
    }
}
