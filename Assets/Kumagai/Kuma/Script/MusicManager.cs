using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] SongDataBaseReon dataBase; // 曲のデータベース
    AudioSource audio; // オーディオソースコンポーネント
    AudioClip Music; // 現在選択されている曲のオーディオクリップ
    string songName; // 現在選択されている曲の名前
    bool played; // 曲が再生されたかどうかを示すフラグ

    private void Start()
    {
        // 現在選択されている曲の名前をデータベースから取得
        songName = dataBase.songData[GManagerReon.instance.songID].songName; //変更点
        audio = GetComponent<AudioSource>(); // AudioSourceコンポーネントを取得
        Music = GetComponent<AudioClip>(); // 現在選択されている曲のオーディオクリップを取得
        played = false; // 再生フラグを初期化
    }

    private void Update()
    {
        // スペースキーが押され、まだ曲が再生されていない場合
        if ((Input.GetKeyUp(KeyCode.Space)) && !played)
        {
            GManagerReon.instance.Start = this; // GManagerでこのインスタンスを開始フラグとして設定
            GManagerReon.instance.StartTime = Time.time; // ゲーム開始時間を設定
            played = true; // 再生フラグを設定
            audio.PlayOneShot(Music); // 曲を再生
        }

    }
}
