using System.Collections.Generic;
using UnityEngine;

namespace Kumagai
{
    public class SoundController : MonoBehaviour
    {
        private Dictionary<string, AudioClip> soundData = new Dictionary<string, AudioClip>();
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            LoadSoundsFromResources();
        }

        private void LoadSoundsFromResources()
        {
            var bgmClips = Resources.LoadAll<AudioClip>("Sound/Bgm");
            var seClips = Resources.LoadAll<AudioClip>("Sound/Se");
            var audioClips = Resources.LoadAll<AudioClip>("Audio");
            
            //BGM
            Debug.Log("BGM取得開始");
            foreach (var clip in bgmClips)
            {
                soundData[clip.name] = clip;
                Debug.Log(clip.name);
            }
            Debug.Log("BGM取得終了");
            //SE
            Debug.Log("SE取得開始");
            foreach (var clip in seClips)
            {
                soundData[clip.name] = clip;
                Debug.Log(clip.name);
            }
            Debug.Log("SEを取得終了");
            //AUDIO
            Debug.Log("AUDIO取得開始");
            foreach (var clip in audioClips)
            {
                soundData[clip.name] = clip;
                Debug.Log(clip.name);
            }
            Debug.Log("AUDIO取得終了");
        }

        public void PlaySound(string soundName)
        {
            if (soundData.TryGetValue(soundName, out var clip))
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
            else
            {
                {
                    Debug.LogError($"Sound '{soundName}' not found in Resources.");
                }
            }
        }

        public void PlaySoundOneShot(string soundName)
        {
            if (soundData.TryGetValue(soundName, out var clip))
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError($"Sound '{soundName}' not found in Resources.");
            }
        }
    }
}