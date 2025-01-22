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
            throw new Exception($"パスからビデオを読み込めませんでした: {videoPath}");
        }
    }

    public void EndGame(int bpm)
    {
        SelectedMusic.Bpm = bpm;
        Debug.Log($"ゲームが終了しました。スコア： {SelectedMusic.Bpm}");
    }
}