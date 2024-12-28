using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;

namespace IdolGame.ApplicationBusinessRules.UseCases;

public sealed class FindIdolGroupDataUseCase
{
    private readonly IAsyncRepository<IdolGroupData, IdolGroupId> repository;

    public FindIdolGroupDataUseCase(IAsyncRepository<IdolGroupData, IdolGroupId> repository)
    {
        this.repository = repository;
    }

    public async UniTask<IEnumerable<IdolGroupData>> FindAllAsync(CancellationToken ct)
    {
        return await repository.FindAllAsync(ct);
    }

    public async UniTask<IdolGroupData?> FindByAsync(IdolGroupId id, CancellationToken ct)
    {
        return await repository.FindAsync(id, ct);
    }
}