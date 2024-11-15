using Kumagai.UseCases;
using UnityEngine;

namespace Kumagai.InterfaceAdapters
{
    public class UnitySoundManager : MonoBehaviour
    {
        private ISoundManager _soundManager;

        void Awake()
        {
            _soundManager = new SoundManager();
        }

        public void LoadSound(string soundId, AudioClip clip)
        {
            _soundManager.LoadSound(soundId, clip);
        }

        public void PlaySound(string soundId)
        {
            _soundManager.PlaySound(soundId);
        }

        public void StopSound(string soundId)
        {
            _soundManager.StopSound(soundId);
        }
    }
}