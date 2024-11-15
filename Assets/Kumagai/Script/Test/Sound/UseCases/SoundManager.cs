using System.Collections.Generic;
using UnityEngine;

namespace Kumagai.UseCases
{
    public interface ISoundManager
    {
        void PlaySound(string soundId);
        void StopSound(string soundId);
        void LoadSound(string soundId, AudioClip clip);
    }

    public class SoundManager:ISoundManager
    { 
        private readonly Dictionary<string, AudioClip> _soundLibrary = new Dictionary<string, AudioClip>();
       
        public void PlaySound(string soundId)
        {
            if (_soundLibrary.TryGetValue(soundId, out var clip))
            {
                // サウンドを再生するロジック（Unityのオーディオシステムを使用）
                AudioSource.PlayClipAtPoint(clip, Vector3.zero);
            }
        }

        public void StopSound(string soundId)
        {
            // サウンドを停止するロジック
            // ここでは、具体的な実装を省略
        }

        public void LoadSound(string soundId, AudioClip clip)
        {
            if (!_soundLibrary.ContainsKey(soundId)) { _soundLibrary[soundId] = clip; }
        }
    }
}