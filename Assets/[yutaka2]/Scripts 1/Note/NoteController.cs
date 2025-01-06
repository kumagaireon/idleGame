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
    private NoteShaker shakeChecker;

    private int generatedGroupsNum = 0;

    private float timer = 0.0f;

    public static List<List<GameObject>> groupList = new List<List<GameObject>>();
    

    private int BPM;
    private int FPS = 60;

    private List<GameObject> clickedList = new List<GameObject>();

    private float noteLifeTime = 2.0f;
    //private Dictionary<int, float> groupSpawnTimes = new Dictionary<int, float>();
    private List<float> groupSpawnTimes = new List<float>();        

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
    void Start()
    {        
        generator = GetComponent<NoteGenerator>();
        positioner = GetComponent<NotePositioner>();
        alphaChanger = GetComponent<NoteAlphaChanger>();  
        tapChecker = GetComponent<NoteTap>();
        
        BPM = CSVReader.instance.BPM;
        Debug.Log(CSVReader.data.Count);
    }

    // Update is called once per frame
    async void Update()
    {
        timer += Time.deltaTime;
        if (generatedGroupsNum >= CSVReader.instance.GetGroupsCount())
        {
            Debug.Log("Finish generatedGroupsNum:" + generatedGroupsNum + "groupCount:" + CSVReader.instance.GetGroupsCount());            
        }

        else if (timer > CSVReader.data[generatedGroupsNum].time)
        {
            switch (CSVReader.data[generatedGroupsNum].TypeOfGroup)
            {
                case 0: ExecuteGroup(generatedGroupsNum); break;    //�ʏ�̃m�[�c
                case 1: ExecuteTap(); break;                          //�^�b�v����
                //�����̊֌W�ŃX���C�v�̏�����                                                                      
            }

            //��������f�[�^���X�V����
            generatedGroupsNum++;

        }

        CheckAndDestroyOldNotes();
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
        for(int i = 0; i < generateNotesNum; i++)
        {
            GameObject noteObj = generator.GenerateNote();
            noteObj.SetActive(false);

            //�O���[�v�ɂ܂Ƃ߂�
            objList.Add(noteObj);
        }
        //�O���[�v���X�g�ɒǉ�
        groupList.Add(objList);
        //******************************

        //******���̂�����*******
        for (int i = 0; i < generateNotesNum; i++)
        {
            if (objList[i] != null)
            {
                //����               
                objList[i].SetActive(true);

                //�ʒu��ݒ�
                objList[i].transform.position = positioner.SetPosition(CSVReader.data[groupNum].position[i]);

                //�A���t�@�l��Fade
                SpriteRenderer noteRenderer = objList[i].GetComponent<SpriteRenderer>();
                alphaChanger.FadeIn(noteRenderer);
            }
            //�ҋ@����                
            await Task.Delay(1000 / (generateNotesNum - 1));
        }
        //*************************

        //�O���[�v�����������L�^
        groupSpawnTimes.Add(Time.time);
    }


    //�������Ԃ���������m�[�c������
    private void CheckAndDestroyOldNotes() {
        float currentTime = Time.time;
        List<int> groupsToRemove = new List<int>();

        foreach(var groupTime in groupSpawnTimes)
        {
            if(currentTime - groupTime >= noteLifeTime)
            {
                int groupNum = groupSpawnTimes.IndexOf(groupTime);
                Debug.Log("timeOverNum:" +  groupNum);
                if(groupNum < groupList.Count)
                {
                    foreach(var note in groupList[groupNum])
                    {
                        if(note != null && note.activeSelf)
                        {
                            note.SetActive(false);
                        }
                    }
                }                
                groupsToRemove.Add(groupNum);
            }
        }

        foreach(var groupIndex in groupsToRemove)
        {
            groupSpawnTimes.Remove(groupIndex);
        }
    }

    private async void ExecuteTap()
    {
        await tapChecker.OnTapAble();
    }

    private async void ExecuteSwipe()
    {
        await shakeChecker.OnShakeAble();
    }
}