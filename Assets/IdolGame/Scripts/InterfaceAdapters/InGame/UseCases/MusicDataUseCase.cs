using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Entities;
using IdolGame.InGame.Interfaces;

namespace IdolGame.InGame.UseCases;

public class MusicDataUseCase
{
    private readonly IMusicCSVDataRepository musicCsvDataRepository;

    // コンストラクタでIMusicDataRepositoryを受け取る
    public MusicDataUseCase(IMusicCSVDataRepository musicCsvDataRepository)
    {
        this.musicCsvDataRepository = musicCsvDataRepository;
    }

    // BPM（拍数）を取得するメソッド
    public int GetBpm()
    {
        return musicCsvDataRepository.GetBpm();
    }
}