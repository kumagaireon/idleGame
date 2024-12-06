using System.Collections.Generic;
using UnityEngine;

public class NoteOrderChecker : MonoBehaviour
{
    public static NoteOrderChecker instance;

    private List<GameObject> clickedList = new List<GameObject>();

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
    public void CheckNoteClick(GameObject targetObj)
    {        
        clickedList.Add(targetObj);
        for (int row = 0; row < NoteController.groupList.Count; row++)
        {
            //�N���b�N���ׂ��I�u�W�F�N�g���ǂ����𔻒肷��i���Ԃ��m���߂�j
            if (NoteController.groupList[row].Contains(targetObj))
            {
                int index = NoteController.groupList[row].IndexOf(targetObj);
                Debug.Log("index:" + index);
                Debug.Log("count:" + clickedList.Count);
                if (clickedList.Count != index + 1)
                {
                    Debug.Log("���s");
                    NoteDestroyer.instance.DestroyObject(targetObj, 1);
                    ClearClickedList();
                }
                else
                {
                    //�I�u�W�F�N�g�����X�g�̍Ōォ�ǂ����𔻒肷��   
                    if (NoteController.groupList[row].Count - 1 == index)
                    {
                        Debug.Log("�Ō�");
                        ClearClickedList();
                    }
                    Debug.Log("����");
                    NoteDestroyer.instance.DestroyObject(targetObj, 0);
                }
            }                     
        }
    }

    public void ClearClickedList()
    {
        clickedList.Clear();
    }

}
