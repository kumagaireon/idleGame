using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.ApplicationBusinessRules.Interfaces;

namespace IdolGame.ApplicationBusinessRules.UseCases;

public class FindSongSelectionDataUseCase
{
    private readonly IAsyncRepository<MusicData,MusicId> repository;

    public FindSongSelectionDataUseCase(IAsyncRepository<MusicData, MusicId> repository)
    {
        this.repository = repository;
    }
    public async UniTask<IEnumerable<MusicData>> FindAllAsync(CancellationToken ct)
    {
        return await repository.FindAllAsync(ct);
    }

    public async UniTask<MusicData?> FindByAsync(MusicId id, CancellationToken ct)
    {
        return await repository.FindAsync(id, ct);
    }
}