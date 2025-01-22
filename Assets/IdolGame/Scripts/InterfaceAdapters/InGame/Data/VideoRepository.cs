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
    // ビデオパスを指定してビデオを非同期で読み込むメソッドの実装
    public async UniTask<VideoClip> LoadVideo(string? videoPath)
    {
        var handle = Addressables.LoadAssetAsync<VideoClip>(videoPath);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        else
        {
            Debug.LogError($"ビデオの読み込みに失敗しました: {videoPath}");
            throw new Exception($"ビデオの読み込みに失敗しました {videoPath}");
        }
    }
}