using System.Collections.Generic;
using UnityEngine;

public class NoteOrderChecker : MonoBehaviour
{
    public static NoteOrderChecker instance;

    private List<GameObject> clickedList = new List<GameObject>();

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
    public int CheckNoteClick(GameObject targetObj)
    {
        clickedList.Add(targetObj);
        for (int row = 0; row < NoteController.groupList.Count; row++)
        {
            //�N���b�N���ׂ��I�u�W�F�N�g���ǂ����𔻒肷��i���Ԃ��m���߂�j
            if (NoteController.groupList[row].Contains(targetObj))
            {
                int index = NoteController.groupList[row].IndexOf(targetObj);
                if (clickedList.Count != index + 1)
                {
                    Debug.Log("���s");
                    ClearClickedList();
                    return 2;
                }
                else
                {
                    //�I�u�W�F�N�g�����X�g�̍Ōォ�ǂ����𔻒肷��   
                    if (NoteController.groupList[row].Count - 1 == index)
                    {
                        Debug.Log("�Ō�");
                        ClearClickedList();
                        return 3;
                    }
                    Debug.Log("����");
                    return 1;
                }
            }
        }
        return 0;
    }

    public void ClearClickedList()
    {
        clickedList.Clear();
    }

}
