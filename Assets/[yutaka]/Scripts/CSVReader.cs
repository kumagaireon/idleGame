using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class CSVReader : MonoBehaviour
{
    #region　インスタンスへのstaticなアクセスポイント
    public static CSVReader Instance
    {
        get { return instance; }
    }
    private static CSVReader instance = null;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    

    public struct MusicData
    {
        public float time;
        public float keepTime;
        public int direction;
        public bool type;
    }

    public static List<MusicData> data = new List<MusicData>();

    //一時的に作成
    public GameObject[] iconPrefab;

    //csvの行数を格納
    public int height = 0;

    //CSVファイルの名前を保存
    string csvFileName ;

    //曲のBPMを保存
    public int BPM { get; private set; }

    public List<MusicData> Music_CSV()
    {
        //一時入力用で毎回初期化する
        MusicData dat = new MusicData();
        List<MusicData> dat_list = new List<MusicData>();

        //Resourcesからcsvを読み込むのに必要
        TextAsset csvFile;

        //読み込んだcsvを格納
        List<string[]> csvData = new List<string[]>();
        
        csvFile = Resources.Load("CSV/" + csvFileName) as TextAsset;
        //csvFile = Resources.Load("CSV/musicData") as TextAsset;   
        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek() > -1)
        {
            string line = reader.ReadLine();

            //,で区切ってcsvに格納
            csvData.Add(line.Split(','));
            height++;
        }

        BPM = int.Parse(csvData[0][3]);

        for(int i = 3; i < height; ++i)
        {
            dat.time = Convert.ToSingle(csvData[i][0]);
            dat.keepTime = Convert.ToSingle(csvData[i][1]);
            dat.direction = int.Parse(csvData[i][2]);
            dat.type = bool.Parse(csvData[i][3]);

            dat_list.Add(dat);
        }
        return dat_list;
    }
    void Start()
    {
        if (csvFileName != null)
        {
            csvFileName = SongSelectReon.csvFileName;
        }
        else
        {
            csvFileName = "musicData0";
        }
        Debug.Log(csvFileName);        
        data = Music_CSV();
    }
}
