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
            //クリックすべきオブジェクトかどうかを判定する（順番を確かめる）
            if (NoteController.groupList[row].Contains(targetObj))
            {
                int index = NoteController.groupList[row].IndexOf(targetObj);
                Debug.Log("index:" + index);
                Debug.Log("count:" + clickedList.Count);
                if (clickedList.Count != index + 1)
                {
                    Debug.Log("失敗");
                    NoteDestroyer.instance.DestroyObject(targetObj, 1);
                    ClearClickedList();
                }
                else
                {
                    //オブジェクトがリストの最後かどうかを判定する   
                    if (NoteController.groupList[row].Count - 1 == index)
                    {
                        Debug.Log("最後");
                        ClearClickedList();
                    }
                    Debug.Log("成功");
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
