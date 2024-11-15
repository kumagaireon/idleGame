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


    public void LogKeepTimeEnd()
    {
        Debug.Log("継続時間が終了しました。"); // 継続時間の終了をログに出力
    }
}
