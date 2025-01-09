using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class AddressablePlayer : MonoBehaviour
{

    private VideoPlayer videoPlayer;

    async UniTask Start()
    {   
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        var voiceHandle = Addressables.LoadAssetAsync<VideoClip>("play_video_1");

        VideoClip videoClip = await voiceHandle.Task;

        videoPlayer.clip = videoClip;
        videoPlayer.playOnAwake = false;

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted
            += (VideoPlayer vp) =>
        {
            vp.Play();
        };

        await UniTask.WaitForSeconds(2.0f);
    }
}