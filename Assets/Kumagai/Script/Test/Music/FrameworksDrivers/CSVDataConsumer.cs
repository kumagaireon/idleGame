using System.Collections.Generic;
using Kumagai.Entities;
using Kumagai.UseCase;
using UnityEngine;

namespace Kumagai.FrameworksDrivers
{
    public class CSVDataConsumer : MonoBehaviour
    {
        private void Start()
        {
            // 保存したCSVデータを取得
            List<MusicDataKari> musicData = DataHolder.Instance.GetMusicData();

            // CSVデータを使用する処理を記述
            if (musicData != null)
            {
                foreach (var data in musicData)
                {
                    Debug.Log(  $"Time: {data.time}, KeepTime: {data.keepTime}, Direction: {data.direction}, Type: {data.type}");
                }
            }
            else
            {
                Debug.LogError("CSVデータが見つかりません");
            }
        }
    }
}