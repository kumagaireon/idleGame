using UnityEngine;

public class NoteDestroyer : MonoBehaviour
{
    public static NoteDestroyer instance;

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


    /// <summary>
    /// オブジェクトの破壊
    /// </summary>
    /// <param name="targetObj">破壊対象のオブジェクト</param>
    /// <param name="type">0: 個別に破壊, 1: グループ全体を破壊</param>
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
    /*{
        // ノートグループリストをループしてクリックされたオブジェクトをチェック
        for (int row = 0; row < NoteController.groupList.Count; row++)
        {
            for (int col = 0; col < NoteController.groupList[row].Count; col++)
            {
                if (NoteController.groupList[row][col] != targetObj) continue;
                switch (type)
                {
                    // オブジェクト個別に破壊する場合
                    case 0:
                        targetObj.SetActive(false);
                        break;
                    // オブジェクトの属するグループ全体を破壊する場合
                    case 1:
                    {
                        // グループ内のすべてのオブジェクトを非アクティブにする
                        foreach (GameObject o in NoteController.groupList[row])
                        {
                            o.SetActive(false);
                        }

                        // グループリストをnullに設定（メモリ管理のため）
                        for (var k = 0; k < NoteController.groupList[row].Count; k++)
                        {
                            NoteController.groupList[row][k] = null;
                        }

                        break;
                    }
                }
            }
        }
    }*/
    /*
     {
        for (int row = 0; row < NoteController.groupList.Count; row++)

        {
            int col = NoteController.groupList[row].IndexOf(targetObj);
            if (col != -1) // 見つかった場合のみ処理を行う
            {
                if (type == 0)
                {
                    DeactivateObject(targetObj);
                }
                else if (type == 1)
                {
                    DeactivateGroup(NoteController.groupList[row]);
                }

                break; // 見つかったらループを抜ける
            }
        }
    }

    private void DeactivateObject(GameObject targetObj)
    {
        targetObj.SetActive(false);
    }

    private void DeactivateGroup(List<GameObject> group)
    {
        foreach (GameObject obj in group)
        {
            obj.SetActive(false);
        }

        for (int k = 0; k < group.Count; k++)
        {
            group[k] = null;
        }
    }
 */
}
