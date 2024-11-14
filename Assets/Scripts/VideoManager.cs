using UnityEngine;
using UnityEngine.Video;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

public class VideoManager : MonoBehaviour
{
    //使う
    public VideoPlayer videoPlayer;
   [SerializeField] private List<string> videoPaths;

    private void Start()
    {
        string videoDirectory = Path.Combine(Application.streamingAssetsPath, "Videos");
        videoPaths = new List<string>(Directory.GetFiles(videoDirectory, "*.mp4"));

        PlayRandomVideo().Forget();
    }

    private async UniTask PlayRandomVideo()
    {
        if (videoPaths.Count == 0)
        {
            Debug.LogError("No video files found in the directory.");
            return;
        }

        // ランダムな動画パスを選択
        int randomIndex = Random.Range(0, videoPaths.Count);
        string selectedVideoPath = videoPaths[randomIndex];
     
        Debug.Log("Playing video: " + selectedVideoPath);

        // 動画を非同期に読み込む
        string url = await LoadVideoAsync(selectedVideoPath);
        videoPlayer.url = url; 
        videoPlayer.Prepare();
        
        // 再生準備完了を待機
        await UniTask.WaitUntil(() => videoPlayer.isPrepared);
        videoPlayer.Play();
    }

    private async UniTask<string> LoadVideoAsync(string path)
    {
        // 動画ファイルを非同期に読み込む
        await UniTask.SwitchToThreadPool(); // メインスレッド以外での読み込みを行う
        string videoPath = Path.Combine(Application.streamingAssetsPath, path);
        await UniTask.Delay(100);// サンプル用の擬似的な遅延を追加
        await UniTask.SwitchToMainThread(); // メインスレッドに戻る
        return videoPath;
    }
}