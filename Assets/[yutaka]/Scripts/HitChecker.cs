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
                //Hit�����I�u�W�F�N�g��ێ�
                currentHitObject = hit.collider.gameObject;                

                NoteOrderChecker.instance.CheckNoteClick(currentHitObject);

                //�I�u�W�F�N�g�P�̂�����
                NoteDestroyer.instance.DestroyObject(currentHitObject, 0);                                         
            }
        }

        if(InputChecker.instance.GetMouseButtonUp())
        {
            //�I�u�W�F�N�g��������O���[�v������
            NoteDestroyer.instance.DestroyObject(currentHitObject, 1);

            //�N���b�N���X�g��Clear
            NoteOrderChecker.instance.ClearClickedList();
        }
    }
}
