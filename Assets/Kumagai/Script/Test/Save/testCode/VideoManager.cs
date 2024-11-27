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
    // VideoPlayerコンポーネントの参照を保持するフィールド
    public VideoPlayer videoPlayer;
    // ビデオファイルのパスを保持するリスト
   [SerializeField] private List<string> videoPaths;

    private void Start()
    {
        // 仮のビデオディレクトリパスを設定
        string videoDirectory = Path.Combine(Application.streamingAssetsPath, "TestVideos");
        // 指定されたディレクトリ内のすべての.mp4ファイルをリストに追加
        videoPaths = new List<string>(Directory.GetFiles(videoDirectory, "*.mp4"));
        // ランダムなビデオを再生する非同期タスクを開始
        PlayRandomVideo().Forget();
    }

    /// <summary>
    /// ランダムなビデオを再生する非同期メソッド
    /// </summary>
    private async UniTask PlayRandomVideo()
    {
        if (videoPaths.Count == 0)
        {
            Debug.LogError("No video files found in the directory.");
            return;
        }

        // ランダムな動画パスを選択
        int randomIndex = Random.Range(0, videoPaths.Count);
        string selectedVideoPath = videoPaths[0];
     
        //Debug.Log("Playing video: " + selectedVideoPath);

        // 動画を非同期に読み込む
        string url = await LoadVideoAsync(selectedVideoPath);
        videoPlayer.url = url; 
        videoPlayer.Prepare();
        
        // 再生準備完了を待機
        await UniTask.WaitUntil(() => videoPlayer.isPrepared);
        videoPlayer.Play();
    }

    /// <summary>
    /// 指定されたパスの動画ファイルを非同期に読み込む
    /// </summary>
    /// <param name="path">読み込む動画ファイルのパス</param>
    /// <returns>読み込まれた動画ファイルの完全なパス</returns>
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