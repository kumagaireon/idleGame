using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    public static LogManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンが変わってもオブジェクトを保持
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LogMusicData(CSVAAAA.MusicData musicData)
    {
        Debug.Log($"時間: {musicData.time}," +
            $"継続時間: {musicData.keepTime}, " +
            $"方向: {musicData.direction}," +
            $"タイプ: {musicData.type}");
    }
    public void LogKeepTimeEnd()
    {
        Debug.Log("継続時間が終了しました。");
    }
}
