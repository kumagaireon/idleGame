using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTestap : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //�����Ƀ^�b�v���ꂽ���̏���������
            Debug.Log("AAAAAAAAAAAA");
        }
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
