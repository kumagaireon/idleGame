using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.Video;

namespace IdolGame.InGame.Interfaces;

public interface IVideoRepository
{
    UniTask<VideoClip> LoadVideo(string videoPath);
}