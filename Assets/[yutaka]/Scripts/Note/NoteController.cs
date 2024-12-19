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
        positioner = GetComponent<NotePositioner>();
        alphaChanger = GetComponent<NoteAlphaChanger>();
        tapChecker = GetComponent<NoteTap>();
        destroyer = GetComponent<NoteDestroyer>();

        BPM = CSVReader.instance.BPM;
        Debug.Log("BPM");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("noteController");
        timer += Time.deltaTime;

        if (CSVReader.data != null && CSVReader.data.Count > 0)
        {
            if (generatedGroupsNum < CSVReader.data.Count)
            {
                Debug.Log($"Current time: {timer}, Next note time: {CSVReader.data[generatedGroupsNum].time}");
            }
        }

        if (generatedGroupsNum >= CSVReader.instance.GetGroupsCount())
        {
            Debug.Log("Finish generatedGroupsNum:" + generatedGroupsNum + "groupCount:" + CSVReader.instance.GetGroupsCount());
        }
        else if (timer > CSVReader.data[generatedGroupsNum].time)
        {
            Debug.Log($"Generating note group {generatedGroupsNum}, Type: {CSVReader.data[generatedGroupsNum].TypeOfGroup}");

            switch (CSVReader.data[generatedGroupsNum].TypeOfGroup)
            {
                case 0:
                    Debug.Log("Executing normal note group");
                    ExecuteGroup(generatedGroupsNum);
                    break;
                case 1:
                    Debug.Log("Executing tap note");
                    ExecuteTap();
                    break;
            }

            //��������f�[�^���X�V����
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
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;
//using UnityEngine;

//public class NoteController : MonoBehaviour
//{
//    public static NoteController instance;

//    private NoteGenerator generator;
//    private NotePositioner positioner;
//    private NoteAlphaChanger alphaChanger;
//    private NoteTap tapChecker;
//    private NoteDestroyer destroyer;

//    private int generatedGroupsNum = 0;

//    private float timer = 0.0f;

//    public static List<List<GameObject>> groupList = new List<List<GameObject>>();


//    private int BPM;
//    private int FPS = 60;

//    private List<GameObject> clickedList = new List<GameObject>();    

//    public static List<float> groupSpawnTimes = new List<float>();        

//    private void Awake()
//    {
//        if(instance == null)
//        {
//            instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }
//    void Start()
//    {        
//        generator = GetComponent<NoteGenerator>();
//        positioner = GetComponent<NotePositioner>();
//        alphaChanger = GetComponent<NoteAlphaChanger>();  
//        tapChecker = GetComponent<NoteTap>();
//        destroyer = GetComponent<NoteDestroyer>();

//        BPM = CSVReader.instance.BPM;
//        Debug.Log("BPM");
//    }

//    // Update is called once per frame
//    async void Update()
//    {
//        timer += Time.deltaTime;

//        if (CSVReader.data != null && CSVReader.data.Count > 0)
//        {
//            if (generatedGroupsNum < CSVReader.data.Count)
//            {
//                Debug.Log($"Current time: {timer}, Next note time: {CSVReader.data[generatedGroupsNum].time}");
//            }
//        }

//        if (generatedGroupsNum >= CSVReader.instance.GetGroupsCount())
//        {
//            Debug.Log("Finish generatedGroupsNum:" + generatedGroupsNum + "groupCount:" + CSVReader.instance.GetGroupsCount());            
//        }
//        else if (timer > CSVReader.data[generatedGroupsNum].time)
//        {
//            Debug.Log($"Generating note group {generatedGroupsNum}, Type: {CSVReader.data[generatedGroupsNum].TypeOfGroup}");

//            switch (CSVReader.data[generatedGroupsNum].TypeOfGroup)
//            {
//                case 0: 
//                    Debug.Log("Executing normal note group");
//                    ExecuteGroup(generatedGroupsNum); 
//                    break;
//                case 1: 
//                    Debug.Log("Executing tap note");
//                    ExecuteTap(); 
//                    break;
//            }

//            //��������f�[�^���X�V����
//            generatedGroupsNum++;
//        }

//        destroyer.CheckAndDestroyOldNotes();
//    }

//    private async void ExecuteGroup(int groupsNum)
//    {
//        await OnExecuteGroup(groupsNum);
//    }

//    private async Task OnExecuteGroup(int groupNum)
//    {
//        //�O���[�v�ŊǗ����邽�߂�List���쐬
//        List<GameObject> objList = new List<GameObject>();

//        //���������
//        int generateNotesNum = CSVReader.data[groupNum].InfoOfGroup;

//        //*****�O���[�v���X�g�ɒǉ�******
//        for(int i = 0; i < generateNotesNum; i++)
//        {
//            GameObject noteObj = generator.GenerateNote();
//            if (noteObj != null)
//            {
//                noteObj.SetActive(false);
//                objList.Add(noteObj);
//            }
//            else
//            {
//                Debug.LogError("Failed to generate note object");
//            }
//        }

//        groupList.Add(objList);
//        //******************************

//        //******���̂�����*******
//        for (int i = 0; i < generateNotesNum; i++)
//        {
//            if (objList[i] != null)
//            {
//                //����               
//                objList[i].SetActive(true);

//                Vector3 position = positioner.SetPosition(CSVReader.data[groupNum].position[i]);
//                objList[i].transform.position = position;
//                Debug.Log($"Note {i} positioned at {position}");

//                //�A���t�@�l��Fade
//                SpriteRenderer noteRenderer = objList[i].GetComponent<SpriteRenderer>();
//                if (noteRenderer != null)
//                {
//                    alphaChanger.FadeIn(noteRenderer);
//                }
//            }
//            await Task.Delay(1000 / (generateNotesNum - 1));
//        }
//        //*************************

//        //�O���[�v�����������L�^
//        groupSpawnTimes.Add(Time.time);
//        Debug.Log($"Completed generation of group {groupNum}");
//    }

//    private async void ExecuteTap()
//    {
//        await tapChecker.OnTapAble();
//    }  
//}