using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SongSelectReon : MonoBehaviour
{
    [SerializeField] SongDataBaseReon dataBase; // 曲のデータベース
    [SerializeField] TextMeshProUGUI[] songNameText; // 曲名を表示するTextMeshProUGUIの配列
    [SerializeField] TextMeshProUGUI[] songLevelText; // 曲レベルを表示するTextMeshProUGUIの配列
    [SerializeField] TextMeshProUGUI[] csvFileText; // 曲レベルを表示するTextMeshProUGUIの配列

    [SerializeField] Image songImage; // 曲の画像を表示するImageオブジェクト

    AudioSource audio; // オーディオソースコンポーネント
    AudioClip Music; // 現在選択されている曲のオーディオクリップ
    string songName; // 現在選択されている曲の名前
    int select; // 現在選択されている曲のインデックス

    public static string csvFileName;

    private void Start()
    {
        select = 0; // 最初に選択される曲をインデックス0に設定
        audio = GetComponent<AudioSource>(); // AudioSourceコンポーネントを取得
        songName = dataBase.songData[select].songName; // 初期の曲名をデータベースから取得
        Music = (AudioClip)Resources.Load("Musics/" + songName); // 初期の曲をリソースからロード
        SongUpdateALL(); // 曲情報を全て更新
    }

    void Update()
    {
        // 下矢印キーが押されたら
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (select < dataBase.songData.Length - 1) // データベースの長さ以内であれば
            {
                select++; // 曲の選択を次の曲に移動
                SongUpdateALL(); // 曲情報を全て更新
            }
        }

        // 上矢印キーが押されたら
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (select > 0) // インデックスが0以上であれば
            {
                select--; // 曲の選択を前の曲に移動
                SongUpdateALL(); // 曲情報を全て更新
            }
        }

        // スペースキーが押されたら
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SongStart(); // 曲の再生を開始
        }
    }

    private void SongUpdateALL()
    {
        songName = dataBase.songData[select].songName; // 現在選択されている曲名をデータベースから取得
        Music = (AudioClip)Resources.Load("Musics/" + songName); // 現在選択されている曲をリソースからロード
        audio.Stop(); // 現在再生中の音楽を停止
        audio.PlayOneShot(Music); // 新しい曲を再生

        for (int i = 0; i < 5; i++)
        {
            SongUpdate(i - 2); // 曲の情報を更新
        }
    }

    private void SongUpdate(int id)
    {
        try
        {
            // 曲名とレベルをテキストに表示
            songNameText[id + 2].text = dataBase.songData[select + id].songName;
            songLevelText[id + 2].text = "Lv." + dataBase.songData[select + id].songLevel;
        }
        catch
        {
            // インデックスが範囲外の場合、テキストを空にする
            songNameText[id + 2].text = "";
            songLevelText[id + 2].text = "";
        }

        if (id == 0)
        {
            songImage.sprite = dataBase.songData[select + id].songImage; // 曲の画像を表示
        }
    }

    public void SongStart()
    {
        GManagerReon.instance.songID = select; // 選択された曲のIDをGManagerに設定
        SceneManager.LoadScene("MusicScene"); // 音楽シーンをロード
    }    
}