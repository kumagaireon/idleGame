using System.Collections.Generic;
using UnityEngine;

public class NoteDestroyer : MonoBehaviour
{
    public static NoteDestroyer instance;

    private float noteLifeTime = 2.0f;
    private List<List<GameObject>> groupList = new List<List<GameObject>>();
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

    private void Start()
    {
        groupList = NoteController.groupList;
        groupSpawnTimes = NoteController.groupSpawnTimes;
    }

    /// <summary>
    /// オブジェクトの消去
    /// </summary>
    /// <param name="targetObj">消去するオブジェクト</param>
    /// <param name="type">単体かグループか（このままならboolでもいい）</param>
    public void DestroyObject(GameObject targetObj, int type)
    {
        for (int row = 0; row < NoteController.groupList.Count; row++)
        {
            for (int col = 0; col < NoteController.groupList[row].Count; col++)
            {
                if (NoteController.groupList[row][col] == targetObj)
                {
                    //オブジェクト単体を消去する場合
                    if (type == 0)
                    {
                        targetObj.SetActive(false);                        
                    }

                    //オブジェクトが所属するグループを消去する場合
                    else if (type == 1)
                    {
                        //表示されているオブジェクトを非アクティブ
                        foreach (GameObject o in NoteController.groupList[row])
                        {
                            o.SetActive(false);
                        }

                        //リストをnullにして表示しないようにする（async awaitが裏で回っているため）
                        for (int k = 0; k < NoteController.groupList[row].Count; k++)
                        {
                            NoteController. groupList[row][k] = null;
                            //groupList[row] = null;
                        }                        
                    }
                }
            }
        }
    }

    //生存時間をすぎたらノーツを消去
    public void CheckAndDestroyOldNotes()
    {        
        float currentTime = Time.time;
        List<int> groupsToRemove = new List<int>();

        foreach (var groupTime in groupSpawnTimes)
        {
            if (currentTime - groupTime >= noteLifeTime)
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
}
