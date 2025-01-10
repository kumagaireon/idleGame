using UnityEngine;

public class HitChecker : MonoBehaviour
{
    private Vector3 mousePosition; // マウスの位置
    private GameObject currentHitObject; // 現在ヒットしているオブジェクト

    void Update()
    {
        // マウスの位置をスクリーン座標からワールド座標に変換
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // マウスの位置にRaycastを飛ばす
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null)
        {
            if (InputChecker.instance.GetMouseButtonDown())
            {
                // ヒットしたオブジェクトを取得
                currentHitObject = hit.collider.gameObject;

                // ヒットしたオブジェクトの順序をチェック (0: 何もなし, 1: 続行, 2: 失敗, 3: 完了)
                int destroyNum = NoteOrderChecker.instance.CheckNoteClick(currentHitObject);

                // 順序に基づいて処理を分岐
                switch (destroyNum)
                {
                    case 0: break;
                    case 1: // オブジェクトを個別に破壊
                        NoteDestroyer.instance.DestroyObject(currentHitObject, 0);
                        ScoreController.instance.GetNoteScore();
                        ;
                        break;
                    case 2: // オブジェクトグループを破壊
                        NoteDestroyer.instance.DestroyObject(currentHitObject, 1); break;
                    case 3: // オブジェクトグループを破壊 (最後のオブジェクト)
                        NoteDestroyer.instance.DestroyObject(currentHitObject, 1);
                        ScoreController.instance.GetNotePerfectScore();
                        break;
                }
            }
        }

        if (InputChecker.instance.GetMouseButtonUp())
        {
            // オブジェクトグループを破壊
            NoteDestroyer.instance.DestroyObject(currentHitObject, 1);

            // クリックリストをクリア
            NoteOrderChecker.instance.ClearClickedList();
        }
    }
}