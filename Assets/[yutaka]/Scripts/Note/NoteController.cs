using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public static NoteController instance;

    private NoteGenerator generator;
    private NotePositioner positioner;
    private NoteAlphaChanger alphaChanger;
    private NoteTap tapChecker;
    private NoteDestroyer destroyer;

    private int generatedGroupsNum = 0;

    private float timer = 0.0f;

    public static List<List<GameObject>> groupList = new List<List<GameObject>>();


    private int BPM;
    private int FPS = 60;

    private List<GameObject> clickedList = new List<GameObject>();

    public static List<float> groupSpawnTimes = new List<float>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        generator = GetComponent<NoteGenerator>();
        if (generator == null)
        {
            Debug.LogError("NoteGenerator component not found!");
            return;
        }

        positioner = GetComponent<NotePositioner>();
        if (positioner == null)
        {
            Debug.LogError("NotePositioner component not found!");
            return;
        }

        if (CSVReader.data == null || CSVReader.data.Count == 0)
        {
            Debug.LogError("No CSV data available at start!");
            return;
        }

        Debug.Log($"NoteController initialized with {CSVReader.data.Count} note groups");

        alphaChanger = GetComponent<NoteAlphaChanger>();
        tapChecker = GetComponent<NoteTap>();
        destroyer = GetComponent<NoteDestroyer>();

        BPM = CSVReader.instance.BPM;
        Debug.Log("BPM");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("noteController"); // 過剰なデバッグログを削除
        timer += Time.deltaTime;

        // データの有効性チェックを強化
        if (CSVReader.data == null || CSVReader.data.Count == 0)
        {
            Debug.LogError("CSVReader.data is null or empty");
            return;
        }

        if (generatedGroupsNum >= CSVReader.data.Count)
        {
            // Debug.Log("All notes generated");
            return;
        }

        // ノーツ生成のタイミングチェック
        if (timer > CSVReader.data[generatedGroupsNum].time)
        {
            float currentTime = CSVReader.data[generatedGroupsNum].time;
            int noteType = (int)CSVReader.data[generatedGroupsNum].TypeOfGroup;
            
            Debug.Log($"Generating note group {generatedGroupsNum} at time {currentTime}, Type: {noteType}");

            switch (noteType)
            {
                case 0:
                    ExecuteGroup(generatedGroupsNum);
                    break;
                case 1:
                    ExecuteTap();
                    break;
                default:
                    Debug.LogError($"Unknown note type: {noteType}");
                    break;
            }

            generatedGroupsNum++;
        }

        destroyer.CheckAndDestroyOldNotes();
    }

    private async void ExecuteGroup(int groupsNum)
    {
        await OnExecuteGroup(groupsNum);
    }

    private async Task OnExecuteGroup(int groupNum)
    {
        //�O���[�v�ŊǗ����邽�߂�List���쐬
        List<GameObject> objList = new List<GameObject>();

        //���������
        int generateNotesNum = CSVReader.data[groupNum].InfoOfGroup;

        //*****�O���[�v���X�g�ɒǉ�******
        for (int i = 0; i < generateNotesNum; i++)
        {
            Debug.Log("generater");
            GameObject noteObj = generator.GenerateNote();
            if (noteObj != null)
            {
                noteObj.SetActive(false);
                objList.Add(noteObj);
            }
            else
            {
                Debug.LogError("Failed to generate note object");
            }
        }

        groupList.Add(objList);
        //******************************

        //******���̂�����*******
        for (int i = 0; i < generateNotesNum; i++)
        {
            if (objList[i] != null)
            {
                //����               
                objList[i].SetActive(true);

                Vector3 position = positioner.SetPosition(CSVReader.data[groupNum].position[i]);
                objList[i].transform.position = position;
                Debug.Log($"Note {i} positioned at {position}");

                //�A���t�@�l��Fade
                SpriteRenderer noteRenderer = objList[i].GetComponent<SpriteRenderer>();
                if (noteRenderer != null)
                {
                    alphaChanger.FadeIn(noteRenderer);
                }
            }
            await Task.Delay(1000 / (generateNotesNum - 1));
        }
        //*************************

        //�O���[�v�����������L�^
        groupSpawnTimes.Add(Time.time);
        Debug.Log($"Completed generation of group {groupNum}");
    }

    private async void ExecuteTap()
    {
        await tapChecker.OnTapAble();
    }
}