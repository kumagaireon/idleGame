using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;

namespace IdolGame.ApplicationBusinessRules.UseCases;

public class FindIdolRewardDataUseCase
{
    private readonly IAsyncRepository<IdolRewardData,IdolId> repository;

    public FindIdolRewardDataUseCase(IAsyncRepository<IdolRewardData, IdolId> repository)
    {
        this.repository = repository;
    }
    public async UniTask<IEnumerable<IdolRewardData>> FindAllAsync(CancellationToken ct)
    {
        return await repository.FindAllAsync(ct);
    }

    // 特定のIDに基づいてアイドルグループを非同期に検索
    public async UniTask<IdolRewardData?> FindByAsync(IdolId id, CancellationToken ct)
    {
        return await repository.FindAsync(id, ct);
    }
}