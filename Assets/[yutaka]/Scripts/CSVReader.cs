using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class CSVReader : MonoBehaviour
{
    #region�@�C���X�^���X�ւ�static�ȃA�N�Z�X�|�C���g
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

    //�ꎞ�I�ɍ쐬
    public GameObject[] iconPrefab;

    //csv�̍s�����i�[
    public int height = 0;

    //CSV�t�@�C���̖��O��ۑ�
    string csvFileName ;

    //�Ȃ�BPM��ۑ�
    public int BPM { get; private set; }

    public List<MusicData> Music_CSV()
    {
        //�ꎞ���͗p�Ŗ��񏉊�������
        MusicData dat = new MusicData();
        List<MusicData> dat_list = new List<MusicData>();

        //Resources����csv��ǂݍ��ނ̂ɕK�v
        TextAsset csvFile;

        //�ǂݍ���csv���i�[
        List<string[]> csvData = new List<string[]>();
        
        csvFile = Resources.Load("CSV/" + csvFileName) as TextAsset;
        //csvFile = Resources.Load("CSV/musicData") as TextAsset;   
        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek() > -1)
        {
            string line = reader.ReadLine();

            //,�ŋ�؂���csv�Ɋi�[
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
