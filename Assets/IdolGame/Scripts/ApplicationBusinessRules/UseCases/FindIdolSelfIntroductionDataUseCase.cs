using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;

namespace IdolGame.ApplicationBusinessRules.UseCases;

public class FindIdolSelfIntroductionDataUseCase
{
    private readonly IAsyncRepository<IdolSelfIntroductionData,IdolId> repository;

    public FindIdolSelfIntroductionDataUseCase(IAsyncRepository<IdolSelfIntroductionData, IdolId> repository)
    {
        this.repository = repository;
    }
    public async UniTask<IEnumerable<IdolSelfIntroductionData>> FindAllAsync(CancellationToken ct)
    {
        return await repository.FindAllAsync(ct);
    }

    // 特定のIDに基づいてアイドルグループを非同期に検索
    public async UniTask<IdolSelfIntroductionData?> FindByAsync(IdolId id, CancellationToken ct)
    {
        return await repository.FindAsync(id, ct);
    }
}