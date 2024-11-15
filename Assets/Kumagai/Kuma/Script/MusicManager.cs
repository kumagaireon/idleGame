using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] SongDataBaseReon dataBase; // 曲データベース
    new AudioSource audio; // オーディオソースコンポーネント
    AudioClip music; // 現在再生する音楽クリップ
    string songName; // 現在再生する曲名
    bool played; // 曲が既に再生されたかを示すフラグ

    private void Start()
    {
        // 曲名をデータベースから取得
        songName = dataBase.songData[GManagerReon.instance.songID].songName; // 曲IDに基づいて曲名を取得
        audio = GetComponent<AudioSource>(); // オーディオソースコンポーネントを取得
        music = GetComponent<AudioClip>(); // オーディオクリップを取得
        played = false; // 再生フラグを初期化
    }

    private void Update()
    {
        // スペースキーが押された時かつ曲がまだ再生されていない場合
        if ((Input.GetKeyUp(KeyCode.Space)) && !played)
        {
            GManagerReon.instance.Start = this;
            ; // GManagerのインスタンスに現在のオブジェクトを設定
            GManagerReon.instance.StartTime = Time.time; // ゲーム開始時間を設定
            played = true; // 再生フラグを設定
            audio.PlayOneShot(music); // 曲を再生
        }
    }
}
