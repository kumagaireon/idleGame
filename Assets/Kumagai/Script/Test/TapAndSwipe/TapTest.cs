using UnityEngine;

public class TapTest : MonoBehaviour
{
    void Update()
    {
#if UNITY_EDITOR
        // PC �̏ꍇ�̓��͏���
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Debug.Log("�}�E�X�N���b�N: " + mousePosition);
        }
#else
        // ���o�C���̓��͏���
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            Debug.Log("�^�b�`���o: " + touchPosition);
        }
#endif
        if (Input.touchCount == 0)
        {
            //��ʂɐG��Ă��Ȃ��ꍇ�̏��� 
        }

        if(OnTouchDown())
        {
            Debug.Log("�^�b�v����܂���");
        }
    }

    //�X�}�z���� ���̃I�u�W�F�N�g���^�b�`����Ă�����true�i�}���`�^�b�v�Ή��j
    bool OnTouchDown()
    {
        // �^�b�`����Ă���Ƃ�
        if (0 < Input.touchCount)
        {
            // �^�b�`����Ă���w�̐���������
            for (int i = 0; i < Input.touchCount; i++)
            {
                // �^�b�`�����R�s�[
                Touch t = Input.GetTouch(i);
                // �^�b�`�����Ƃ����ǂ���
                if (t.phase == TouchPhase.Began)
                {
                    //�^�b�`�����ʒu����Ray���΂�
                    Ray ray = Camera.main.ScreenPointToRay(t.position);
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit))
                    {
                        //Ray���΂��Ă��������I�u�W�F�N�g���������g��������
                        if (hit.collider.gameObject == this.gameObject)
                        {
                            return true;
                        }
                    }
                    Debug.Log("�^�b�`���ꂽ");
                    return true;
                }
            }
        }
        return false; //�^�b�`����ĂȂ�������false
    }
}
