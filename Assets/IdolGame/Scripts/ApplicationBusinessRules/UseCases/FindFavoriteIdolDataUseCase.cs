using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;
using IdolGame.EnterpriseBusinessRules;

namespace IdolGame.ApplicationBusinessRules.UseCases;

public sealed class FindFavoriteIdolDataUseCase
{
    private readonly IAsyncRepository<IdolGroup, IdolGroupId> repository;

    public FindFavoriteIdolDataUseCase(IAsyncRepository<IdolGroup, IdolGroupId> repository)
    {
        this.repository = repository;
    }

    // 全てのアイドルグループを非同期に取得
    public async UniTask<IEnumerable<IdolGroup>> FindAllAsync(CancellationToken ct)
    {
        return await repository.FindAllAsync(ct);
    }

    // 特定のIDに基づいてアイドルグループを非同期に検索
    public async UniTask<IdolGroup?> FindByAsync(IdolGroupId id, CancellationToken ct)
    {
        return await repository.FindAsync(id, ct);
    }
}