using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;

namespace IdolGame.ApplicationBusinessRules.UseCases;

public sealed class FindFavoriteIdolDataUseCase
{
    private readonly IAsyncRepository<IdolGroupData, IdolGroupId> repository;

    public FindFavoriteIdolDataUseCase(IAsyncRepository<IdolGroupData, IdolGroupId> repository)
    {
        this.repository = repository;
    }

    // 全てのアイドルグループを非同期に取得
    public async UniTask<IEnumerable<IdolGroupData>> FindAllAsync(CancellationToken ct)
    {
        return await repository.FindAllAsync(ct);
    }

    // 特定のIDに基づいてアイドルグループを非同期に検索
    public async UniTask<IdolGroupData?> FindByAsync(IdolGroupId id, CancellationToken ct)
    {
        return await repository.FindAsync(id, ct);
    }
}