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
        public int type;
    }

    public static List<MusicData> data = new List<MusicData>();

    //�ꎞ�I�ɍ쐬
    public GameObject[] iconPrefab;

    //csv�̍s�����i�[
    public int height = 0;

    public List<MusicData> Music_CSV()
    {
        //�ꎞ���͗p�Ŗ��񏉊�������
        MusicData dat = new MusicData();
        List<MusicData> dat_list = new List<MusicData>();

        //Resources����csv��ǂݍ��ނ̂ɕK�v
        TextAsset csvFile;

        //�ǂݍ���csv���i�[
        List<string[]> csvData = new List<string[]>();        

        csvFile = Resources.Load("CSV/musicData") as TextAsset;   
        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek() > -1)
        {
            string line = reader.ReadLine();

            //,�ŋ�؂���csv�Ɋi�[
            csvData.Add(line.Split(','));
            height++;
        }

        for(int i = 1; i < height; ++i)
        {
            dat.time = Convert.ToSingle(csvData[i][0]);
            dat.keepTime = Convert.ToSingle(csvData[i][1]);
            dat.type = int.Parse(csvData[i][2]);

            dat_list.Add(dat);
        }
        return dat_list;
    }



    // Start is called before the first frame update
    void Start()
    {
        data = Music_CSV();       
    }
}
