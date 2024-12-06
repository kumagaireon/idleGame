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

                NoteOrderChecker.instance.CheckNoteClick(currentHitObject);

                //オブジェクト単体を消去
                NoteDestroyer.instance.DestroyObject(currentHitObject, 0);                                         
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
