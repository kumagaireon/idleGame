using System;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;

namespace IdolGame.InGame.Data;

public class VideoRepository : IVideoRepository
{
    public async UniTask<VideoClip> LoadVideo(string videoPath)
    {
        AsyncOperationHandle<VideoClip> handle = Addressables.LoadAssetAsync<VideoClip>(videoPath);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        else
        {
            Debug.LogError($"Failed to load video: {videoPath}");
            throw new Exception($"Failed to load video: {videoPath}");
        }
    }
}