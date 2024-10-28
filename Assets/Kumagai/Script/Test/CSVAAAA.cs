using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class CSVAAAA : MonoBehaviour
{
    #region インスタンスへのstaticなアクセスポイント
    public static CSVAAAA Instance
    {
        get { return instance; }
    }
    private static CSVAAAA instance = null;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public struct MusicData
    {
        public float time;      //いつ
        public float keepTime;  //継続時間
        public int direction;   //方向(1:2:)
        public bool type;       //判定の種類(true:false:)
    }
    public static List<MusicData> data = new List<MusicData>();
    int height = 0;
    string csvFileName;

    public List<MusicData> Music_CSV()
    {
        MusicData dat = new MusicData();
        List<MusicData> dat_list = new List<MusicData>();
        TextAsset csvFile = Resources.Load("CSV/" + csvFileName) as TextAsset;
        List<string[]> csvData = new List<string[]>();
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvData.Add(line.Split(','));
            height++;
        }

        // csvDataの中身をログで出力
      /*  for (int i = 3; i < height; i++)
        {
            string[] row = csvData[i];
            Debug.Log("Row " + i + ": " + string.Join(", ", row));
        }
*/
        for (int i = 3; i < height; ++i)
        {
            dat.time = Convert.ToSingle(csvData[i][0]);         
            dat.keepTime = Convert.ToSingle(csvData[i][1]);
            dat.direction = int.Parse(csvData[i][2]);
            dat.type = bool.Parse(csvData[i][3]);
            dat_list.Add(dat);
        }
        return dat_list;
    }

    public void csvAAAA()
    {
        csvFileName = SelectCSV.csvFileName;
        Debug.Log(csvFileName);
        data = Music_CSV();
    }
}