using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace IdolGame.InGame.Presenters;

public class VideoPresenter
{
    private readonly RawImage _rawImage;
    private readonly VideoPlayer _videoPlayer;

    public VideoPresenter(RawImage rawImage, VideoPlayer videoPlayer)
    {
        _rawImage = rawImage;
        _videoPlayer = videoPlayer;
    }

    public void PrepareVideo(VideoClip videoClip)
    {
        var renderTexture = new RenderTexture(1920, 1080, 0);
        _videoPlayer.targetTexture = renderTexture;
        _rawImage.texture = renderTexture;
        _videoPlayer.clip = videoClip;
        _videoPlayer.playOnAwake = false;
    }

    public void PlayVideo()
    {
        _videoPlayer.Prepare();
        _videoPlayer.prepareCompleted += (vp) => vp.Play();
    }

    public void StopVideo()
    {
        _videoPlayer.Stop();
    }
}