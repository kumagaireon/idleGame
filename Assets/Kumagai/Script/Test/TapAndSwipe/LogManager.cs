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
            Destroy(gameObject);// すでにインスタンスが存在する場合はこのオブジェクトを破棄
        }
    }

    public void LogMusicData(CSVAAAA.MusicData musicData)
    {
        Debug.Log("時間:" + musicData.time); // 音楽の再生時間をログに出力
        Debug.Log("継続時間:" + musicData.keepTime); // 継続時間をログに出力
        Debug.Log("方向:" + musicData.direction); // 方向をログに出力
        Debug.Log("タイプ:" + musicData.type); // タイプをログに出力
    }

    public void LogKeepTimeEnd()
    {
        Debug.Log("継続時間が終了しました。"); // 継続時間の終了をログに出力
    }
}
