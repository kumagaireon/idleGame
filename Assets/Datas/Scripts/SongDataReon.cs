using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject を作成するためのメニュー項目を追加

[CreateAssetMenu(fileName ="SongData",menuName ="楽曲データを作成")]
public class SongDataReon : ScriptableObject
{
    // 楽曲データのプロパティ
    public string songID;     // 楽曲のID
    public string songName;   // 楽曲の名前
    public int songLevel;     // 楽曲の難易度レベル
    public Sprite songImage;  // 楽曲の画像
}
