using System;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Data;
using IdolGame.InGame.UseCases;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace IdolGame.InGame.Controllers
{
    public class VideoController : MonoBehaviour
    {
        [SerializeField] private RawImage videoImage;
        [SerializeField] private Text videoNameText;
        private VideoPlayer _videoPlayer;
        private VideoUseCase _videoUseCase;

        private void Awake()
        {
            _videoPlayer = gameObject.AddComponent<VideoPlayer>();
            _videoUseCase = new VideoUseCase(new VideoRepository());
        }

        public async UniTask PlayVideoAsync(VideoClip videoClip)
        {
            try
            {
                var renderTexture = new RenderTexture(1920, 1080, 0);
                _videoPlayer.targetTexture = renderTexture;
                videoImage.texture = renderTexture;
                _videoPlayer.clip = videoClip;
                _videoPlayer.playOnAwake = false;
                if (videoNameText != null)
                {
                    videoNameText.text = videoClip.name;
                }

                _videoPlayer.Prepare();
                _videoPlayer.prepareCompleted += (vp) => vp.Play();
                _videoPlayer.loopPointReached += async (vp) => await OnVideoEnd(vp);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to play video: {ex.Message}");
            }
        }


        private async UniTask OnVideoEnd(VideoPlayer vp)
        {
            Debug.Log("動画が終了しました。");
            await UniTask.Delay(2000);
            Debug.Log("遷移します");
            // SceneManager.LoadSceneAsync("NextScene"); // 次のシーンへの遷移
        }

        public void StopVideo()
        {
            if (_videoPlayer == null)
            {
                Debug.LogError("VideoPlayer is not initialized.");
                return;
            }

            _videoPlayer.Stop();
        }
    }
}