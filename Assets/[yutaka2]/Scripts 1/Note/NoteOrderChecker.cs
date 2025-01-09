using System.Collections.Generic;
using UnityEngine;

public class NoteOrderChecker : MonoBehaviour
{
    public static NoteOrderChecker instance;

    // クリックされたオブジェクトのリスト
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

    // クリックされたノートをチェックするメソッド
    public int CheckNoteClick(GameObject targetObj)
    {
        // クリックされたオブジェクトをリストに追加
        clickedList.Add(targetObj);
        // ノートグループリストをループしてクリックされたオブジェクトをチェック
        for (int row = 0; row < NoteController.groupList.Count; row++)
        {
            // 指定されたオブジェクトがグループに含まれているかチェック
            if (NoteController.groupList[row].Contains(targetObj))
            {
                int index = NoteController.groupList[row].IndexOf(targetObj);

                // クリックされた順序が正しいかチェック
                if (clickedList.Count != index + 1)
                {
                    Debug.Log("失敗");
                    ClearClickedList();
                    return 2; // 失敗のコード
                }
                else
                {
                    // グループ内の最後のオブジェクトかどうかをチェック
                    if (NoteController.groupList[row].Count - 1 == index)
                    {
                        Debug.Log("成功");
                        ClearClickedList();
                        return 3; // 成功のコード
                    }

                    Debug.Log("進行中");
                    return 1; // 正しい順序だがまだ完了していない
                }
            }
        }

        return 0; // 何も起こらなかった場合
    }

    // クリックされたリストをクリアするメソッド
    public void ClearClickedList()
    {
        clickedList.Clear();
    }
}