using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public static NoteController? instance;

    private NoteGenerator? generator;
    private NotePositioner? positioner;
    private NoteAlphaChanger? alphaChanger;
    private NoteTap? tapChecker;
    private NoteShaker? shakeChecker;

    private int generatedGroupsNum = 0; // 生成されたグループの数

    private float timer = 0.0f; // タイマー

    // ノートのグループリスト
    public static List<List<GameObject>> groupList = new List<List<GameObject>>();


    private int BPM;
    private int FPS = 60;

    private float noteLifeTime = 2.0f; // ノートの寿命時間

    // クリックされたノートリスト
    private List<GameObject> clickedList = new List<GameObject>();

    //private Dictionary<int, float> groupSpawnTimes = new Dictionary<int, float>();
    // グループの生成時間リスト
    private List<float> groupSpawnTimes = new List<float>();

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
        shakeChecker = GetComponent<NoteShaker>();

        BPM = CSVReader.instance!.Bpm;
        Debug.Log(BPM);
    }


    async void Update()
    {
        // タイマーの更新
        timer += Time.deltaTime * (BPM / 60.0f);
        // すべてのグループが生成された場合
        if (generatedGroupsNum >= CSVReader.instance!.GetGroupsCount())
        {
            Debug.Log("Finish generatedGroupsNum:" + generatedGroupsNum + "groupCount:" +
                      CSVReader.instance.GetGroupsCount());
        }
        // グループ生成のタイミングに達した場合
        else if (timer > CSVReader.data[generatedGroupsNum].Time)
        {
            switch (CSVReader.data[generatedGroupsNum].TypeOfGroup)
            {
                case 0: ExecuteGroup(generatedGroupsNum); break; // ノートグループ
                case 1: ExecuteTap(); break; // タップアクション
                case 2:
                    ExecuteSwipe();
                    break;
                    ; // スワイプアクション
                // 他のアクションも追加可能
            }

            // 生成されたグループ数を更新
            generatedGroupsNum++;

        }

        // 古いノートをチェックして削除
        CheckAndDestroyOldNotes();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private async void ExecuteGroup(int groupsNum)
    {
        await OnExecuteGroup(groupsNum);
    }

    private async UniTask OnExecuteGroup(int groupNum)
    {
        // グループを管理するリストを作成
        List<GameObject> objList = new List<GameObject>();

        // 生成するノートの数
        int generateNotesNum = CSVReader.data[groupNum].InfoOfGroup;

        // グループリストに追加
        for (int i = 0; i < generateNotesNum; i++)
        {
            GameObject noteObj = generator!.GenerateNote();
            noteObj.SetActive(false);

            objList.Add(noteObj);
        }

        // ノートを表示
        groupList.Add(objList);

        for (int i = 0; i < generateNotesNum; i++)
        {
            if (objList[i] != null)
            {
                objList[i].SetActive(true);
                objList[i].transform.position = positioner!.SetPosition(CSVReader.data[groupNum].Position[i]);

                SpriteRenderer noteRenderer = objList[i].GetComponent<SpriteRenderer>();
                alphaChanger!.FadeIn(noteRenderer);
            }

            await Task.Delay((60 * 1000 / BPM) / (generateNotesNum - 1));
        }

        // グループ生成時間を記録
        groupSpawnTimes.Add(Time.time);
    }


    // 古いノートをチェックして削除
    // ReSharper disable Unity.PerformanceAnalysis
    private void CheckAndDestroyOldNotes()
    {
        float currentTime = Time.time;
        List<int> groupsToRemove = new List<int>();

        foreach (var groupTime in groupSpawnTimes)
        {
            if (currentTime - groupTime >= noteLifeTime * (60.0f / BPM))
            {
                int groupNum = groupSpawnTimes.IndexOf(groupTime);
                Debug.Log("timeOverNum:" + groupNum);
                if (groupNum < groupList.Count)
                {
                    foreach (var note in groupList[groupNum])
                    {
                        if (note != null && note.activeSelf)
                        {
                            note.SetActive(false);
                            ScoreController.instance.MinusNoteScore();
                        }
                    }
                }

                groupsToRemove.Add(groupNum);
            }
        }

        foreach (var groupIndex in groupsToRemove)
        {
            groupSpawnTimes.Remove(groupIndex);
        }
    }

    // タップアクションを実行
    private void ExecuteTap()
    {
        tapChecker!.StartTabAble();
    }

    // スワイプアクションを実行
    private async void ExecuteSwipe()
    {
        if (!shakeChecker)
        {
            Debug.Log("null");
        }

        await shakeChecker!.OnShakeAble();
    }
}