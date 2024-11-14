using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.Video;

public class VideoManager2:MonoBehaviour
{
    public VideoPlayer videoPlayer;
   [SerializeField] private List<string> videoAddresses = new List<string>{};

    private void Start()
    {
        // ランダムな動画をプリロードし再生
        LoadAndPlayRandomVideo().Forget();
    }

    private async UniTask LoadAndPlayRandomVideo()
    {
        if (videoAddresses.Count == 0)
        {
            Debug.LogError("No video addresses found.");
            return;
        }

        // ランダムなインデックスを生成
        int randomIndex = Random.Range(0, videoAddresses.Count);
        string selectedVideoAddress = videoAddresses[randomIndex];

        Debug.Log("video: " + videoAddresses.Count);
        Debug.Log("Loading video: " + selectedVideoAddress);

        // 動画のアセットハンドルを取得
        AsyncOperationHandle<IList<IResourceLocation>> handle 
            = Addressables.LoadResourceLocationsAsync(selectedVideoAddress);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            IList<IResourceLocation> locations = handle.Result;
            if (locations.Count > 0)
            {
                // 動画をストリーミング再生
                string path = locations[0].InternalId; // 動画のパスを取得
                videoPlayer.url = Path.Combine(Application.streamingAssetsPath, path);
                videoPlayer.Prepare();
                await UniTask.WaitUntil(() => videoPlayer.isPrepared);
                videoPlayer.Play();
            }
        }
        else
        {
            Debug.LogError("Failed to load resource locations: " + selectedVideoAddress);
        }
    }
}