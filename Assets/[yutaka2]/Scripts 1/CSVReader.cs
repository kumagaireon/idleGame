using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;


public class CSVReader : MonoBehaviour
{
    public static CSVReader instance;

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
    }

    private void Start()
    {
        csvFileName = MoviePassGetter.videoFileName;
        Debug.Log("CSVFileName:" + csvFileName);
        data = Music_CSV();
    }

    public void getCSVFileName(string fileName)
    {
        csvFileName = fileName;
        Debug.Log("CSVFileName:" +  csvFileName);
    }

    public struct MusicData
    {
        public float time;
        public float TypeOfGroup;
        public int InfoOfGroup;
        public List<int> position;
    }

    public static List<MusicData> data = new List<MusicData>();

    private int height = 0;
    private int startHeight = 5;
    private string csvFileName;
    public string CsvFileName
    {
        get => csvFileName;
        set => csvFileName = value;
    }
    public int BPM { get; private set; }

    public List<MusicData> Music_CSV()
    {
        //一時入力用で毎回初期化する
        List<MusicData> dat_list = new List<MusicData>();

        //Resourcesからcsvファイルを読み込むため
        TextAsset csvFile;
        Debug.Log(csvFileName);
        csvFile = Resources.Load("CSV/" + csvFileName) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        //読み込んだcsvの内容を格納
        List<string[]> csvData = new List<string[]>();
        while(reader.Peek() > -1)
        {
            string line = reader.ReadLine();

            //,で区切って格納
            csvData.Add(line.Split(','));
            height++;
        }

        BPM = int.Parse(csvData[1][3]);        

        for(int i = startHeight - 1; i < height; ++i)
        {
            //1行のデータ保存用の変数
            MusicData dat = new MusicData();       
            
            //*****各値の保存*****
            dat.time = Convert.ToSingle(csvData[i][0]);            
            //dat.InfoOfGroup = int.Parse(csvData[i][1]);
            dat.TypeOfGroup = int.Parse(csvData[i][2]);
            dat.InfoOfGroup = int.Parse(csvData[i][3]);           

            //int positionStartIndex = 2;
            int positionStartIndex = 4;
            
            //positionのデータは複数存在するためListを作成
            dat.position = new List<int>();
            for (int j = 0; j < dat.InfoOfGroup; ++j)
            {
                //位置は4列目から
                dat.position.Add(int.Parse(csvData[i][j + positionStartIndex]));                
            }
            //********************
            dat_list.Add(dat);
        }                
        return dat_list;
    }

    public int GetGroupsCount()
    {        
        return height - startHeight + 1;
    }
}
