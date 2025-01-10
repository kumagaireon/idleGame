using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;

namespace IdolGame.InGame.Models;

public class SceneModel
{
    public SelectedMusicData SelectedMusic { get; set; }

    public SceneModel(SelectedMusicData selectedMusic)
    {
        SelectedMusic = selectedMusic;
    }

    public string GetVideoPath()
    {
        return SelectedMusic.VideoPath;
    }

    public string? GetNotePath()
    {
        return SelectedMusic.CsvPath;
    }

    public async UniTask<VideoClip> LoadVideoAsync()
    {
        var videoPath = GetVideoPath();
        var videoHandle = Addressables.LoadAssetAsync<VideoClip>(videoPath);
        await videoHandle.Task;
        if (videoHandle.Status == AsyncOperationStatus.Succeeded)
        {
            return videoHandle.Result;
        }
        else
        {
            throw new Exception($"Failed to load video from path: {videoPath}");
        }
    }

    public void EndGame(float score)
    {
        SelectedMusic.Score = score;
        Debug.Log($"Game ended. Score: {SelectedMusic.Score}");
    }
}