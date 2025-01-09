using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class CSVReader : MonoBehaviour
{
    public static CSVReader instance;
    [SerializeField] private MoviePassGetter moviePassGetter;

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
        if (csvFileName == null)
        {
            csvFileName = MoviePassGetter.videoFileName;
        }        
        data = Music_CSV();
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
        //�ꎞ���͗p�Ŗ��񏉊�������
        List<MusicData> dat_list = new List<MusicData>();

        //Resources����csv�t�@�C����ǂݍ��ނ���        
        TextAsset csvFile;
        Debug.Log(csvFileName);
        csvFile = Resources.Load("CSV/" + csvFileName) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        //�ǂݍ���csv�̓��e���i�[
        List<string[]> csvData = new List<string[]>();
        while(reader.Peek() > -1)
        {
            string line = reader.ReadLine();

            //,�ŋ�؂��Ċi�[
            csvData.Add(line.Split(','));
            height++;
        }

        BPM = int.Parse(csvData[1][3]);        

        for(int i = startHeight - 1; i < height; ++i)
        {
            //1�s�̃f�[�^�ۑ��p�̕ϐ�
            MusicData dat = new MusicData();       
            
            //*****�e�l�̕ۑ�*****
            dat.time = Convert.ToSingle(csvData[i][0]);            
            //dat.InfoOfGroup = int.Parse(csvData[i][1]);
            dat.TypeOfGroup = int.Parse(csvData[i][2]);
            dat.InfoOfGroup = int.Parse(csvData[i][3]);           

            //int positionStartIndex = 2;
            int positionStartIndex = 4;
            
            //position�̃f�[�^�͕������݂��邽��List���쐬
            dat.position = new List<int>();
            for (int j = 0; j < dat.InfoOfGroup; ++j)
            {
                //�ʒu��4��ڂ���
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
