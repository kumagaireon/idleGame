using System.Collections.Generic;
using Kumagai.Entities;
using UnityEngine;

namespace Kumagai.UseCase
{
    // DataHolderクラスは、シーン間でデータを保持するシングルトンパターンの実装を提供する
    public class DataHolder : MonoBehaviour
    {
        // DataHolderクラスのシングルトンインスタンス
        public static DataHolder Instance { get; private set; }

        // MusicDataKariオブジェクトのリストを保持するフィールド
        private List<MusicDataKari> musicData;

        private void Awake()
        {
            if (Instance == null)
            {
                // インスタンスが存在しない場合、自身をインスタンスとして設定し、シーン間でオブジェクトを保持
                Instance = this;
                DontDestroyOnLoad(gameObject); // シーンをまたいでオブジェクトを保持
            }
            else
            {
                // 既存のインスタンスが存在する場合、このオブジェクトを破棄
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// musicDataフィールドにデータを設定する
        /// </summary>
        /// <param name="data"></param>
        public void SetMusicData(List<MusicDataKari> data)
        {
            musicData = data;
        }

        /// <summary>
        /// musicDataフィールドからデータを取得する
        /// </summary>
        /// <returns></returns>
        public List<MusicDataKari> GetMusicData()
        {
            return musicData;
        }
    }
}