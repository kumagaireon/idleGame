using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.Video;

namespace IdolGame.InGame.Interfaces;

public interface IVideoRepository
{
    // ビデオパスを指定してビデオを非同期で読み込むメソッド
    UniTask<VideoClip> LoadVideo(string? videoPath);
}