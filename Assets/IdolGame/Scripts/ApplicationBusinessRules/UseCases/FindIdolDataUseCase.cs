using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;

namespace IdolGame.ApplicationBusinessRules.UseCases;

public sealed class FindIdolDataUseCase
{
    private readonly IAsyncRepository<IdolMembersData, IdolId> repository;

    public FindIdolDataUseCase(IAsyncRepository<IdolMembersData, IdolId> repository)
    {
        this.repository = repository;
    }

    public async UniTask<IEnumerable<IdolMembersData>> FindAllAsync(CancellationToken ct)
    {
        return await repository.FindAllAsync(ct);
    }

    public async UniTask<IdolMembersData?> FindByAsync(IdolId id, CancellationToken ct)
    {
        return await repository.FindAsync(id, ct);
    }
}