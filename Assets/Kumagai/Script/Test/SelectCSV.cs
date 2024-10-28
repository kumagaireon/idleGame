using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectCSV : MonoBehaviour
{
    [SerializeField] SongDataBaseReon dataBase; // 曲のデータベース
    [SerializeField] string songName; // 現在選択されている曲の名前
    int select; // 現在選択されている曲のインデックス
    [SerializeField] public static string csvFileName; // CSVファイル名

    private void Start()
    {
        select = 0;
        songName = dataBase.songData[select].songName;
        csvFileName = dataBase.songData[select].csvFileName;
        SongUpdateALL();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (select < dataBase.songData.Length - 1)
            {
                select++;
                SongUpdateALL();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (select > 0)
            {
                select--;
                SongUpdateALL();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SongStart();
        }
    }

    private void SongUpdateALL()
    {
        songName = dataBase.songData[select].songName;
        csvFileName = dataBase.songData[select].csvFileName;
    }

    public void SongStart()
    {
        CSVPlayer.csvFileName = dataBase.songData[select].csvFileName;
        CSVAAAA.Instance.csvAAAA(); // CSVデータを再読み込み
        CSVPlayer.Instance.StartPlayback(); // 再生開始
        Debug.Log(csvFileName);
    }
}