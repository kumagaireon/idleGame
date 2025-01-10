using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoControllerAkama : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public RenderTexture renderTexture;

    void Start()
    {
        videoPlayer.targetTexture = renderTexture;
        rawImage.texture = renderTexture;
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath,
            ButtonClick.videoToPlay + ".mp4");
        videoPlayer.url = videoPath;
        videoPlayer.Play();
    }
}
