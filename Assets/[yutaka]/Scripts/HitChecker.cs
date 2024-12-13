using UnityEngine;

public class HitChecker : MonoBehaviour
{
    private Vector3 mousePosition;
    private GameObject currentHitObject;
    
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if(hit.collider != null )
        {
            if(InputChecker.instance.GetMouseButtonDown())
            {
                //Hitしたオブジェクトを保持
                currentHitObject = hit.collider.gameObject;                

                //Hitしたオブジェクトの種類（0:成功、1:失敗、2:最後)
                int destroyNum = NoteOrderChecker.instance.CheckNoteClick(currentHitObject);

                switch(destroyNum)
                {
                    case 0: break;
                    case 1: //オブジェクト単体を消去
                        NoteDestroyer.instance.DestroyObject(currentHitObject, 0);
                        ScoreController.instance.GetNoteScore(); ;break;
                    case 2: //オブジェクトグループを消去
                        NoteDestroyer.instance.DestroyObject(currentHitObject, 1); break;
                    case 3: //オブジェクトグループを消去(最後のオブジェクト)
                        NoteDestroyer.instance.DestroyObject(currentHitObject, 1);
                        ScoreController.instance.GetNotePerfectScore(); break;
                }                
            }
        }

        if(InputChecker.instance.GetMouseButtonUp())
        {
            //オブジェクトが属するグループを消去
            NoteDestroyer.instance.DestroyObject(currentHitObject, 1);

            //クリックリストをClear
            NoteOrderChecker.instance.ClearClickedList();
        }
    }
}
