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
    /// �I�u�W�F�N�g�̏���
    /// </summary>
    /// <param name="targetObj">��������I�u�W�F�N�g</param>
    /// <param name="type">�P�̂��O���[�v���i���̂܂܂Ȃ�bool�ł������j</param>
    public void DestroyObject(GameObject targetObj, int type)
    {
        for (int row = 0; row < NoteController.groupList.Count; row++)
        {
            for (int col = 0; col < NoteController.groupList[row].Count; col++)
            {
                if (NoteController.groupList[row][col] == targetObj)
                {
                    //�I�u�W�F�N�g�P�̂���������ꍇ
                    if (type == 0)
                    {
                        targetObj.SetActive(false);                        
                    }

                    //�I�u�W�F�N�g����������O���[�v����������ꍇ
                    else if (type == 1)
                    {
                        //�\������Ă���I�u�W�F�N�g���A�N�e�B�u
                        foreach (GameObject o in NoteController.groupList[row])
                        {
                            o.SetActive(false);
                        }

                        //���X�g��null�ɂ��ĕ\�����Ȃ��悤�ɂ���iasync await�����ŉ���Ă��邽�߁j
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

    //�������Ԃ���������m�[�c������
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
