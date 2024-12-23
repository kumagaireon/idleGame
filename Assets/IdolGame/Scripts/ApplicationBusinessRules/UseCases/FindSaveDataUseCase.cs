using System.Collections.Generic;
using System.Threading;
using IdolGame.ApplicationBusinessRules.Interfaces;
using Cysharp.Threading.Tasks;

namespace IdolGame.ApplicationBusinessRules.UseCases;

public sealed class FindSaveDataUseCase
{
    private readonly IAsyncRepository<SaveData, SaveDataId> repository;

    public FindSaveDataUseCase(IAsyncRepository<SaveData, SaveDataId> repository)
    {
        this.repository = repository;
    }

    public async UniTask<IEnumerable<SaveData>> FindAllAsync(CancellationToken ct)
    {
        return  await repository.FindAllAsync(ct);
    }

    public async UniTask<SaveData?> FindByAsync(SaveDataId id, CancellationToken ct)
    {
        return await repository.FindAsync(id, ct);
    }
}