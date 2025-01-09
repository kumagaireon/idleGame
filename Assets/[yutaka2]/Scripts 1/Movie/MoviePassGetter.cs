using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;

// Video Playerを使用するために必要
[DefaultExecutionOrder(-1)]
[RequireComponent(typeof(VideoPlayer))]
public class MoviePassGetter : MonoBehaviour
{
    private VideoPlayer videoPlayer; // Video Playerコンポーネントの参照
    public static AsyncOperationHandle<VideoClip> videoFileName { get; set; } // 動画ファイル名

    async UniTask Awake()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        VideoClip videoClip = await videoFileName.Task;

        videoPlayer.clip = videoClip;
        videoPlayer.playOnAwake = false;

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted
            += (VideoPlayer vp) => { vp.Play(); };

        await UniTask.WaitForSeconds(2.0f);
    }
}