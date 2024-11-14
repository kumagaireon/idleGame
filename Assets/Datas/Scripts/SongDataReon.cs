using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObjectを作成するためのメニュー項目を追加

[CreateAssetMenu(fileName ="SongData",menuName ="音楽データを作成")]
public class SongDataReon : ScriptableObject
{
    // 音楽データのプロパティ
    public string songID;     // 音楽のID
    public string songName;   // 音楽の名前
    public int songLevel;     // 音楽の難易度レベル
    public string csvFileName;     // 音楽のCSVファイル名
    public string selectedVideoPath;     // 動画のPath仮
    public Sprite songImage;  // 音楽の画像    
}
