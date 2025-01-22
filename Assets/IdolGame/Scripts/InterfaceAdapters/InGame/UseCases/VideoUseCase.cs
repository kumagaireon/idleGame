using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;

namespace IdolGame.InGame.UseCases;

public class VideoUseCase
{
    private readonly IVideoRepository _videoRepository;

    public VideoUseCase(IVideoRepository videoRepository)
    {
        _videoRepository = videoRepository;
    }

    public async UniTask<VideoClip> GetVideoClip(string? videoPath)
    {
        return await _videoRepository.LoadVideo(videoPath);
    }
}