using System.Collections.Generic;
using Kumagai.Entities;
using UnityEngine;

namespace Kumagai.UseCase
{
    public class DataHolder : MonoBehaviour
    {
        public static DataHolder Instance { get; private set; } 
        private List<MusicData> musicData;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // シーンをまたいでオブジェクトを保持
            }
            else
            {
                Destroy(gameObject);
                
            } 
        }

        public void SetMusicData(List<MusicData> data)
        {
            musicData = data;
        }

        public List<MusicData> GetMusicData()
        {
            return musicData;
        }
    }
}