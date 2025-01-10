using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.Video;

// Video Playerを使用するために必要
[DefaultExecutionOrder(-1)]
[RequireComponent(typeof(VideoPlayer))]
public class MoviePassGetter : MonoBehaviour
{
    private VideoPlayer videoPlayer; // Video Playerコンポーネントの参照
    public static AsyncOperationHandle<VideoClip> videoFileName { get; set; } // 動画ファイル名
    private RawImage videoImage;
    [SerializeField] private Text videoName;

    async UniTask Awake()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoImage = gameObject.GetComponent<RawImage>();

        VideoClip videoClip = await videoFileName.Task;

        RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
        videoPlayer.targetTexture = renderTexture;

        videoImage.texture = renderTexture;

        Debug.Log($"曲名: {videoClip.name}");

        if (videoName != null)
        {
            videoName.text = videoClip.name;
        }

        await UniTask.WaitForSeconds(3.0f);

        videoName.text = null;

        Debug.Log($"開始: {videoClip.name}");

        videoPlayer.clip = videoClip;
        videoPlayer.playOnAwake = false;

        videoPlayer.Prepare();
        
        videoPlayer.prepareCompleted
            += (VideoPlayer vp) => { vp.Play(); };
        
        videoPlayer.loopPointReached 
            += async (VideoPlayer source) 
                => await OnVideoEnd(); // 動画終了時のイベントにサブスクライブ
        
        await UniTask.WaitForSeconds(2.0f);
    }

    private async UniTask OnVideoEnd()
    {
        Debug.Log("動画が終了しました。");
        await UniTask.WaitForSeconds(2.0f);
        Debug.Log("遷移します");
        // SceneManager.LoadSceneAsync("InGame");
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= async (VideoPlayer source) 
                => await OnVideoEnd(); // スクリプトが破棄される際にイベントのサブスクライブを解除
        }
    }
}