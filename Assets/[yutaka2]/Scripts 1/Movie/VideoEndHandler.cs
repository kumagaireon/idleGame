using UnityEngine;
using UnityEngine.Video;

//仮の終了処理
public class VideoEndHandler : MonoBehaviour
{
   private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd; // イベントにサブスクライブ
    }

    void OnVideoEnd(VideoPlayer source)
    {
        Debug.Log("動画が終了しました。");
    //    SceneManager.LoadSceneAsync("InGame");
    }

    void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoEnd; // スクリプトが破棄される際にイベントのサブスクライブを解除
    }
}