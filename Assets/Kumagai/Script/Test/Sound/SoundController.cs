using System.Collections.Generic;
using UnityEngine;

namespace Kumagai
{
    // SoundControllerクラスは、サウンドの読み込みと再生を管理する
    public class SoundController : MonoBehaviour
    {
        // サウンドデータを格納するディクショナリ
        private Dictionary<string, AudioClip> soundData = new Dictionary<string, AudioClip>();
        // サウンドを再生するためのAudioSource
        private AudioSource audioSource;

        private void Awake()
        {
            // AudioSourceをコンポーネントとして追加
            audioSource = gameObject.AddComponent<AudioSource>();
            // リソースからサウンドを読み込む
            LoadSoundsFromResources();
        }

        /// <summary>
        /// リソースフォルダからサウンドを読み込むメソッド
        /// </summary>
        private void LoadSoundsFromResources()
        {
            // BGM、SE、および一般的なオーディオクリップをロード
            var bgmClips = Resources.LoadAll<AudioClip>("Sound/Bgm");
            var seClips = Resources.LoadAll<AudioClip>("Sound/Se");
            var audioClips = Resources.LoadAll<AudioClip>("Audio");

            // BGMクリップをディクショナリに追加
            //    Debug.Log("BGM取得開始");
            foreach (var clip in bgmClips)
            {
                soundData[clip.name] = clip;
                //  Debug.Log(clip.name);
            }
            //    Debug.Log("BGM取得終了");

            // SEクリップをディクショナリに追加
            //   Debug.Log("SE取得開始");
            foreach (var clip in seClips)
            {
                soundData[clip.name] = clip;
                //  Debug.Log(clip.name);
            }
            //    Debug.Log("SEを取得終了");

            // 一般的なオーディオクリップをディクショナリに追加
            //    Debug.Log("AUDIO取得開始");
            foreach (var clip in audioClips)
            {
                soundData[clip.name] = clip;
                //  Debug.Log(clip.name);
            }
            //   Debug.Log("AUDIO取得終了");
        }

        /// <summary>
        /// 指定されたサウンド名のサウンドを再生するメソッド
        /// </summary>
        /// <param name="soundName">再生するサウンドの名前</param>
        public void PlaySound(string soundName)
        {
            // 指定されたサウンド名のサウンドデータを取得して再生
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

        /// <summary>
        /// 指定されたサウンド名のサウンドを一度だけ再生するメソッド
        /// </summary>
        /// <param name="soundName"></param>
        public void PlaySoundOneShot(string soundName)
        {
            // 指定されたサウンド名のサウンドデータを取得して一度だけ再生
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