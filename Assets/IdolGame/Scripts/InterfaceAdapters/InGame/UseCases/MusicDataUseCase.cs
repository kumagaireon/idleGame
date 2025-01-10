using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Entities;
using IdolGame.InGame.Interfaces;

namespace IdolGame.InGame.UseCases;

public class MusicDataUseCase
{
    private readonly IMusicDataRepository _musicDataRepository;

    public MusicDataUseCase(IMusicDataRepository musicDataRepository)
    {
        _musicDataRepository = musicDataRepository;
    }

    public UniTask<List<CSVMusicData>> GetMusicDataAsync(string? csvFileName)
    {
        return _musicDataRepository.LoadMusicDataAsync(csvFileName);
    }

    public int GetBpm()
    {
        return _musicDataRepository.GetBpm();
    }
}