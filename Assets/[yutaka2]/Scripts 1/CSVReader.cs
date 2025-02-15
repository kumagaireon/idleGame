using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    // シングルトンインスタンス
    public static CSVReader? instance;

    // 音楽データの構造体
    public struct MusicData
    {
        public float Time; // 時間
        public float TypeOfGroup; // グループのタイプ
        public int InfoOfGroup; // グループ情報
        public List<int> Position; // ポジション
    }

    // 音楽データのリスト
    public static List<MusicData> data = new List<MusicData>();

    private int height = 0;// CSVの行数
    private int startHeight = 5;// データの開始行数
    
    private string csvFileName;// CSVファイル名
    
    public int Bpm { get; private set; }// BPM

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // デフォルトのCSVファイル名を設定
        if (csvFileName == null)
        {
            csvFileName = "Fruity_100percent";
          //  csvFileName = "OneLove_FirstLove";
             //   csvFileName = "Ryo_FunTime";
        }        
        
        // CSVデータの読み込み
        data = Music_CSV();
    }
    
    // 音楽データを読み込むメソッド
    public List<MusicData> Music_CSV()
    {
        List<MusicData> datList = new List<MusicData>();

        // ResourcesフォルダからCSVファイルを読み込む  
        TextAsset? csvFile = Resources.Load<TextAsset>($"CSV/{csvFileName}");
        if (csvFile == null)
        {
            Debug.LogError($"CSVファイルが見つかりません: {csvFileName}");
            return datList;
        }

        StringReader reader = new StringReader(csvFile.text);

        // CSVファイルの内容を行ごとにリストに追加
        List<string[]> csvData = new List<string[]>();
        while (reader.Peek() > -1)
        {
            string? line = reader.ReadLine();
            if (line != null)
                csvData.Add(line.Split(','));
            height++;
        }

        // BPMを設定
        Bpm = int.Parse(csvData[1][3]);        

        // 音楽データをリストに追加
        for(int i = startHeight - 1; i < height; ++i)
        {
            // 1行のデータを保持するための変数
            MusicData dat = new MusicData();       
            
            // *****データの保存*****
            dat.Time = Convert.ToSingle(csvData[i][0]);            
            //dat.InfoOfGroup = int.Parse(csvData[i][1]);
            dat.TypeOfGroup = int.Parse(csvData[i][2]);
            dat.InfoOfGroup = int.Parse(csvData[i][3]);           

            //int positionStartIndex = 2;
            int positionStartIndex = 4;
            
            // positionのデータを格納するためのListを作成
            dat.Position = new List<int>();
            for (int j = 0; j < dat.InfoOfGroup; ++j)
            {
                // 位置を4列目から読み取る
                dat.Position.Add(int.Parse(csvData[i][j + positionStartIndex]));                
            }
            //********************
            datList.Add(dat);
        }                
        return datList;
    }

    // グループ数を取得するメソッド
    public int GetGroupsCount()
    {        
        return height - startHeight + 1;
    }
}