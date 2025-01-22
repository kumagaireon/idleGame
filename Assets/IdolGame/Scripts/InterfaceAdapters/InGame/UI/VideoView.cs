using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace IdolGame.InGame.UI
{
    public class VideoView : MonoBehaviour
    {
        [SerializeField] private RawImage videoImage;
        [SerializeField] private Text videoNameText;
        private VideoPlayer videoPlayer;

        private void Awake()
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
        }

        public void PlayVideo(VideoClip videoClip)
        {
            RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
            videoPlayer.targetTexture = renderTexture;
            videoImage.texture = renderTexture;
            videoPlayer.clip = videoClip;
            videoPlayer.playOnAwake = false;
            if (videoNameText != null)
            {
                videoNameText.text = videoClip.name;
            }

            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += (vp) => vp.Play();
        }

        public void StopVideo()
        {
            if (videoPlayer == null)
            {
                Debug.LogError("VideoPlayerが初期化されていません。");
                return;
            }

            videoPlayer.Stop();
        }
    }
}