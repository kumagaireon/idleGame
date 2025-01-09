using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;

// Video Playerを使用するために必要
[DefaultExecutionOrder(-1)]
[RequireComponent(typeof(VideoPlayer))]
public class MoviePassGetter : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer; // Video Playerコンポーネントの参照
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
    /*void Awake()
    {   
        if (videoPlayer == null)
        {
            // Video Playerコンポーネントが設定されていない場合は、自動的に取得
            videoPlayer = GetComponent<VideoPlayer>();                     
        }
        videoFileName = "fruity_100percent";
        realVideoFileName = videoFileName + ".mp4";
        
        //videoPlayer.url = realVideoFileName;
        //videoPlayer.Play();
        
        PlayVideoByFileName(realVideoFileName);        
    }

    /// <summary>
    /// ファイル名を指定して動画を再生
    /// </summary>
    /// <param name="fileName">再生する動画のファイル名（拡張子含む）</param>
    public void PlayVideoByFileName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("ファイル名が指定されていません");
            return;
        }

        
        // StreamingAssetsフォルダ内の動画パスを設定
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        
        // 動画ファイルの存在確認
        if (!System.IO.File.Exists(videoPath))
        {
            Debug.LogError($"動画ファイルが見つかりません: {videoPath}");
            return;
        }

        // 動画のURLを設定して再生
        videoPlayer.url = videoPath;
        videoPlayer.Play();
    }
        */
