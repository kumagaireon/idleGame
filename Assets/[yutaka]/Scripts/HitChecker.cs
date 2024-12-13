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

                //Hit�����I�u�W�F�N�g�̎�ށi0:�����A1:���s�A2:�Ō�)
                int destroyNum = NoteOrderChecker.instance.CheckNoteClick(currentHitObject);

                switch(destroyNum)
                {
                    case 0: break;
                    case 1: //�I�u�W�F�N�g�P�̂�����
                        NoteDestroyer.instance.DestroyObject(currentHitObject, 0);
                        ScoreController.instance.GetNoteScore(); ;break;
                    case 2: //�I�u�W�F�N�g�O���[�v������
                        NoteDestroyer.instance.DestroyObject(currentHitObject, 1); break;
                    case 3: //�I�u�W�F�N�g�O���[�v������(�Ō�̃I�u�W�F�N�g)
                        NoteDestroyer.instance.DestroyObject(currentHitObject, 1);
                        ScoreController.instance.GetNotePerfectScore(); break;
                }                
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
