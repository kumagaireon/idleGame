using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    // 曲のデータを保存するクラス
    public string name; // 曲名
    public int maxBlock; // 最大ブロック数
    public int BPM; // 曲のBPM（Beats Per Minute）
    public int offset; // 曲のオフセット
    public Note[] notes; // ノーツの配列
}

[Serializable]
public class Note
{
    // ノーツデータを保存するクラス
    public int type; // ノーツの種類
    public int num; // ノーツの番号
    public int block; // ノーツが落ちるブロック番号
    public int LPB; // 小節あたりのノーツ数
}
public class NotesManager : MonoBehaviour
{
    public int noteNum;//総ノーツ数
    private string songName;//曲名を入れる変数

    public List<int> LaneNum = new List<int>();//何番目のレーンにノーツが落ちてぅるか
    public List<int> NoteType = new List<int>();//何ノーツか(ロングノーツとか)
    public List<float> NotesTime = new List<float>();//ノーツが判定線と重ねっている時間
    public List<GameObject> NotesObj = new List<GameObject>();//ノーツオブジェクト

    [SerializeField] private float NotesSpeed;//ノーツのスピード

    [SerializeField] GameObject noteObj;//ノーツのPrefab入れる
    [SerializeField] SongDataBaseReon dataBase; // 曲のデータベース
    void OnEnable()
    {
        NotesSpeed = GManagerReon.instance.noteSpeed; // ノーツのスピードを取得
        noteNum = 0; // 総ノーツ数を0に初期化
        songName = dataBase.songData[GManagerReon.instance.songID].songName; // プレイする曲名を取得
        Load(songName); // 曲データをロード
    }

    private void Load(string SongName)
    {
        // Jsonファイルを読み込み
        string inputString = Resources.Load<TextAsset>(SongName).ToString(); // 曲のJsonデータを読み込み
        Data inputJson = JsonUtility.FromJson<Data>(inputString); // Jsonデータをオブジェクトに変換
        noteNum = inputJson.notes.Length; // 総ノーツ数を設定
        GManagerReon.instance.maxScore = noteNum * 5; // 最大スコアを設定

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            // 一小節の長さを計算
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            // ノーツ間の長さを計算
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            // ノーツの降ってくる時間を計算
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            NotesTime.Add(time); // ノーツの時間をリストに追加
            LaneNum.Add(inputJson.notes[i].block); // ノーツのブロック番号をリストに追加
            NoteType.Add(inputJson.notes[i].type); // ノーツの種類をリストに追加
            float z = NotesTime[i] * NotesSpeed; // ノーツの位置を計算
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z), Quaternion.identity)); // ノーツオブジェクトを生成
        }
    }
}