using System.Collections.Generic;
using UnityEngine;

namespace Kumagai
{
    public class SoundManager : MonoBehaviour
    {
        // SE用の最大チャンネル数を定義（シリアライズされる定数）
        [SerializeField] private const int SE_CHANNEL_NUM = 8;

        // 各種AudioSourceを定義
        [SerializeField] private AudioSource[] seSources = new AudioSource[SE_CHANNEL_NUM]; // SE用
        [SerializeField] private AudioSource bgmSource; // BGM用
        [SerializeField] private AudioSource voiceSource; // ボイス用

        // 音声データを格納するDictionary
        [SerializeField] private Dictionary<string, AudioClip> seData = new Dictionary<string, AudioClip>(); // SE用データ
        [SerializeField] private Dictionary<string, AudioClip> bgmData = new Dictionary<string, AudioClip>(); // BGM用データ

        [SerializeField]
        private Dictionary<string, AudioClip> voiceData = new Dictionary<string, AudioClip>(); // ボイス用データ

        // シングルトンインスタンス
        [SerializeField] public static SoundManager Instance;

        private void Awake()
        {
            // シングルトンパターンの実装
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // シーン切り替え時に破棄されないように設定
            }
            else
            {
                Destroy(gameObject); // 既存のインスタンスがある場合は破棄
            }

            // 各AudioSourceを初期化
            for (int i = 0; i < seSources.Length; i++)
            {
                seSources[i] = gameObject.AddComponent<AudioSource>(); // SE用AudioSourceを追加
            }

            bgmSource = gameObject.AddComponent<AudioSource>(); // BGM用AudioSourceを追加
            voiceSource = gameObject.AddComponent<AudioSource>(); // ボイス用AudioSourceを追加

            // BGM用AudioSourceをループ再生に設定
            bgmSource.loop = true;

            // Resourcesフォルダから音声データをロード
            var seClips = Resources.LoadAll<AudioClip>("Sound/Se"); // SE用音声データ
            var bgmClips = Resources.LoadAll<AudioClip>("Sound/Bgm"); // BGM用音声データ
            var voiceClips = Resources.LoadAll<AudioClip>("Sound/Voice"); // ボイス用音声データ

            // 音声データを各Dictionaryに登録
            for (int i = 0; i < seClips.Length; i++)
            {
                seData[seClips[i].name] = seClips[i];
            }

            for (int i = 0; i < bgmClips.Length; i++)
            {
                bgmData[bgmClips[i].name] = bgmClips[i];
            }

            for (int i = 0; i < voiceClips.Length; i++)
            {
                voiceData[voiceClips[i].name] = voiceClips[i];
            }
        }

        // SE再生
        public void PlaySe(string name)
        {
            // 指定された名前のSEデータが存在するかチェック
            if (!seData.ContainsKey(name))
            {
                Debug.LogError("SE data isn't exist: " + name);
                return;
            }

            // 空いているAudioSourceで再生
            foreach (AudioSource source in seSources)
            {
                if (!source.isPlaying)
                {
                    source.clip = seData[name];
                    source.Play();
                    return;
                }
            }
        }

        // SEを一回だけ再生
        public void PlaySeOneShot(string name)
        {
            // 指定された名前のSEデータが存在するかチェック
            if (!seData.ContainsKey(name))
            {
                Debug.LogError("SEデータは存在しません: " + name);
                return;
            }

            // 空いているAudioSourceで一回だけ再生
            foreach (AudioSource source in seSources)
            {
                if (!source.isPlaying)
                {
                    source.clip = seData[name];
                    source.PlayOneShot(source.clip);
                    return;
                }
            }
        }

        // すべてのSEを停止
        public void StopAllSe()
        {
            foreach (AudioSource source in seSources)
            {
                source.Stop();
                source.clip = null;
            }
        }

        // BGM再生（デフォルトの音量）
        public void PlayBgm(string name)
        {
            PlayBgm(name, 1f); // デフォルト音量1で再生
        }

        // BGM再生（音量指定）
        public void PlayBgm(string name, float volume)
        {
            // 指定された名前のBGMデータが存在するかチェック
            if (!bgmData.ContainsKey(name))
            {
                Debug.LogError("BGMデータは存在しません: " + name);
                return;
            }

            // 既に再生中のBGMが同じ場合は処理しない
            if (bgmSource.clip == bgmData[name])
            {
                return;
            }

            // BGMを停止し、新しいBGMをセットして再生
            bgmSource.volume = Mathf.Clamp01(volume); // 音量を0〜1にクランプ
            bgmSource.volume =
                Mathf.Clamp01(
                    volume); // 音量を0〜1にクランプ bgmSource.Stop(); bgmSource.clip = bgmData[name]; bgmSource.Play();
        }

        // BGM停止
        public void StopBgm()
        {
            bgmSource.Stop();
            bgmSource.clip = null;
        }

        // ボイス再生
        public void PlayVoice(string name)
        {
            // 指定された名前のボイスデータが存在するかチェック
            if (!voiceData.ContainsKey(name))
            {
                Debug.LogError("音声データは存在しません: " + name);
                return;
            }

            // ボイスを停止し、新しいボイスをセットして再生
            voiceSource.Stop();
            voiceSource.clip = voiceData[name];
            voiceSource.Play();

        }

        // ボイス停止
        public void StopVoice()
        {
            voiceSource.Stop();
            voiceSource.clip = null;
        }
    }
}