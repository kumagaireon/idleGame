using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Data;
using IdolGame.InGame.Entities;
using IdolGame.InGame.Models;
using IdolGame.InGame.Presenters;
using IdolGame.InGame.UseCases;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.Video;

namespace IdolGame.InGame.Controllers
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] RawImage rawImage;
        [SerializeField] Text videoText;
        [SerializeField] NoteController noteController;
        
        private VideoPlayer videoPlayer;
        private VideoPresenter videoPlayerPresenter;
        private VideoUseCase videoUseCase;
        private MusicCsvDataRepository musicCsvDataRepository;
        
        private string? videoName;
        private string? csvNamePath;
        private string? videoPath;

        private void Awake()
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
            videoPlayerPresenter = new VideoPresenter(rawImage, videoPlayer);
            videoUseCase = new VideoUseCase(new VideoRepository());
            musicCsvDataRepository = new MusicCsvDataRepository();
        }

        //CSVファイルパスとビデオパスを取得し、ビデオを再生
        async void Start()
        {
            if (!string.IsNullOrEmpty(GameData.SelectedMusicData.Name))
            {
                videoName = GameData.SelectedMusicData.Name;
            }
            if (!string.IsNullOrEmpty(GameData.SelectedMusicData.CsvPath))
            {
                csvNamePath = GameData.SelectedMusicData.CsvPath;
            }
            if (!string.IsNullOrEmpty(GameData.SelectedMusicData.VideoPath))
            {
                videoPath = GameData.SelectedMusicData.VideoPath;
            }
            videoText.text = videoName.ToString();
            await UniTask.WaitForSeconds(3.0f);
            videoText.text = string.Empty;
            
            noteController.bpm = GameData.SelectedMusicData.Bpm;
            try
            {
                var musicData = await musicCsvDataRepository.LoadMusicDataAsync(csvNamePath, CancellationToken.None);
                ProcessCsvData(musicData,noteController.bpm);
                var videoClip = await videoUseCase.GetVideoClip(videoPath);
                videoPlayerPresenter.PrepareVideo(videoClip);
                videoPlayerPresenter.PlayVideo();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        // CSVデータを処理し、NoteControllerに渡すメソッド
        private void ProcessCsvData(List<CSVMusicData> musicData,int bpm)
        {
            noteController.InitializeWithMusicData(musicData,bpm);
        }
    }
}