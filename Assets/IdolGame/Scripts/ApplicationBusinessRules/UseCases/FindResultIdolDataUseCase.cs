using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;

namespace IdolGame.ApplicationBusinessRules.UseCases;

public class FindResultIdolDataUseCase
{
    private readonly IAsyncRepository<ResultIdolData,IdolId> repository;

    public FindResultIdolDataUseCase(IAsyncRepository<ResultIdolData, IdolId> repository)
    {
        this.repository = repository;
    }
    public async UniTask<IEnumerable<ResultIdolData>> FindAllAsync(CancellationToken ct)
    {
        return await repository.FindAllAsync(ct);
    }

    // 特定のIDに基づいてアイドルグループを非同期に検索
    public async UniTask<ResultIdolData?> FindByAsync(IdolId id, CancellationToken ct)
    {
        return await repository.FindAsync(id, ct);
    }
}