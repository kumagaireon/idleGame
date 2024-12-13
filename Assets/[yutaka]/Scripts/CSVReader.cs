using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;


public class CSVReader : MonoBehaviour
{
    public static CSVReader instance;
    private bool isInitialized = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���Ԃŕێ�
            Debug.Log("CSVReader instance initialized");
        }
        else
        {
            Debug.Log("Destroying duplicate CSVReader instance");
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (!isInitialized)
        {
            InitializeCSV();
        }
    }

    private void InitializeCSV()
    {
        try
        {
            if (string.IsNullOrEmpty(csvFileName))
            {
                csvFileName = "MusicAndNotes1";
                Debug.Log($"Using default CSV file name: {csvFileName}");
            }

            // Resources �t�H���_�̃p�X���m�F
            string resourcePath = "CSV/" + csvFileName;
            Debug.Log($"Attempting to load from path: {resourcePath}");

            // �t�@�C���̑��݊m�F
            TextAsset testFile = Resources.Load<TextAsset>(resourcePath);
            if (testFile == null)
            {
                Debug.LogError($"CSV file not found at path: {resourcePath}");
                return;
            }

            data = Music_CSV();

            if (data != null && data.Count > 0)
            {
                isInitialized = true;
                Debug.Log($"Successfully loaded {data.Count} music data entries");
                Debug.Log($"First entry - Time: {data[0].time}, Type: {data[0].TypeOfGroup}");
            }
            else
            {
                Debug.LogError("Failed to load music data - data list is empty");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error during initialization: {e.Message}\n{e.StackTrace}");
        }
    }

    public List<MusicData> Music_CSV()
    {
        if (string.IsNullOrEmpty(csvFileName))
        {
            Debug.LogError("CSV filename is null or empty");
            return new List<MusicData>();
        }

        TextAsset csvFile = Resources.Load<TextAsset>("CSV/" + csvFileName);
        if (csvFile == null)
        {
            Debug.LogError($"Failed to load CSV file: {csvFileName}");
            return new List<MusicData>();
        }

        List<MusicData> dat_list = new List<MusicData>();
        height = 0;  // ���Z�b�g

        try
        {
            using (StringReader reader = new StringReader(csvFile.text))
            {
                List<string[]> csvData = new List<string[]>();
                while (reader.Peek() > -1)
                {
                    string line = reader.ReadLine();
                    csvData.Add(line.Split(','));
                    height++;
                }

                if (height < 2)
                {
                    Debug.LogError("CSV file is empty or malformed");
                    return dat_list;
                }

                BPM = int.Parse(csvData[1][2]);
                Debug.Log($"Loaded BPM: {BPM}");

                // ���ۂ̃f�[�^�ǂݍ��ݏ���
                for (int i = startHeight - 1; i < height; ++i)
                {
                    MusicData dat = new MusicData();

                    dat.time = Convert.ToSingle(csvData[i][0]);
                    dat.TypeOfGroup = int.Parse(csvData[i][1]);
                    dat.InfoOfGroup = int.Parse(csvData[i][2]);

                    int positionStartIndex = 3;

                    dat.position = new List<int>();
                    for (int j = 0; j < dat.InfoOfGroup; ++j)
                    {
                        dat.position.Add(int.Parse(csvData[i][j + positionStartIndex]));
                    }
                    dat_list.Add(dat);
                }

                return dat_list;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error reading CSV: {e.Message}");
            return new List<MusicData>();
        }
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
    public int BPM { get; private set; }

    public int GetGroupsCount()
    {
        return height - startHeight + 1;
    }
}


//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;
//using UnityEngine.EventSystems;


//public class CSVReader : MonoBehaviour
//{
//    public static CSVReader instance;

//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//            Debug.Log("CSVReader instance initialized");
//        }
//        else
//        {
//            Debug.Log("Destroying duplicate CSVReader instance");
//            Destroy(gameObject);
//        }
//    }

//    public struct MusicData
//    {
//        public float time;
//        public float TypeOfGroup;
//        public int InfoOfGroup;
//        public List<int> position;
//    }

//    public static List<MusicData> data = new List<MusicData>();

//    private int height = 0;
//    private int startHeight = 5;
//    private string csvFileName;
//    public int BPM { get; private set; }

//    public void Music_CSV()
//    {
//        //�ꎞ���͗p�Ŗ��񏉊�������
//        List<MusicData> dat_list = new List<MusicData>();

//        //Resources����csv�t�@�C����ǂݍ��ނ���
//        TextAsset csvFile;        
//        csvFile = Resources.Load("CSV/" + csvFileName) as TextAsset;
//        if(csvFile != null)
//        {
//            Debug.Log("null���႙�Ȃ�");
//        }
//        else
//        {
//            Debug.Log("null");
//        }
//        StringReader reader = new StringReader(csvFile.text);

//        //�ǂݍ���csv�̓��e���i�[
//        List<string[]> csvData = new List<string[]>();
//        while(reader.Peek() > -1)
//        {
//            string line = reader.ReadLine();

//            //,�ŋ�؂��Ċi�[
//            csvData.Add(line.Split(','));
//            height++;
//        }

//        BPM = int.Parse(csvData[1][2]);        

//        for(int i = startHeight - 1; i < height; ++i)
//        {
//            //1�s�̃f�[�^�ۑ��p�̕ϐ�
//            MusicData dat = new MusicData();       

//            //*****�e�l�̕ۑ�*****
//            dat.time = Convert.ToSingle(csvData[i][0]);            
//            //dat.InfoOfGroup = int.Parse(csvData[i][1]);
//            dat.TypeOfGroup = int.Parse(csvData[i][1]);
//            dat.InfoOfGroup = int.Parse(csvData[i][2]);           

//            //int positionStartIndex = 2;
//            int positionStartIndex = 3;

//            //position�̃f�[�^�͕������݂��邽��List���쐬
//            dat.position = new List<int>();
//            for (int j = 0; j < dat.InfoOfGroup; ++j)
//            {
//                //�ʒu��3��ڂ���
//                dat.position.Add(int.Parse(csvData[i][j + positionStartIndex]));                
//            }
//            //********************
//            dat_list.Add(dat);
//        }                
//        data =  dat_list;
//    }


//    private void Start()
//    {
//        if(csvFileName == null)
//        {
//            csvFileName = "MusicAndNotes1";
//        }
//        Invoke("Music_CSV", 0.1f);            
//    }

//    public int GetGroupsCount()
//    {        
//        return height - startHeight + 1;
//    }
//}
