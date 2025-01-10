using System;
using System.Collections.Generic;
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

        private VideoPlayer videoPlayer; private VideoPresenter videoPlayerPresenter;
        private VideoUseCase videoUseCase; private MusicDataRepository musicDataRepository;

        private string? csvNamePath;
        private string? videoPath;
        private string? videoName;

        private void Awake()
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
            videoPlayerPresenter = new VideoPresenter(rawImage, videoPlayer);
            videoUseCase = new VideoUseCase(new VideoRepository());
            musicDataRepository = new MusicDataRepository();
        }

        async void Start()
        {
            if (!string.IsNullOrEmpty(GameData.SelectedMusicData.CsvPath))
            {
                csvNamePath = GameData.SelectedMusicData.CsvPath;
            }

            if (!string.IsNullOrEmpty(GameData.SelectedMusicData.VideoPath))
            {
                videoPath = GameData.SelectedMusicData.VideoPath;
            }

            videoName = GameData.SelectedMusicData.Name;
            videoText.text = videoName.ToString();
            await UniTask.WaitForSeconds(3.0f);
            videoText.text = string.Empty;
            try
            {
                var musicData = await musicDataRepository.LoadMusicDataAsync(csvNamePath);
                ProcessCsvData(musicData);
                var videoClip = await videoUseCase.GetVideoClip(videoPath);
                videoPlayerPresenter.PrepareVideo(videoClip);
                videoPlayerPresenter.PlayVideo();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        private void ProcessCsvData(List<CSVMusicData> musicData)
        {
            noteController.InitializeWithMusicData(musicData);
        }
    }
}