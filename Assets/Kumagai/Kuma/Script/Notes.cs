using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] float NoteSpeed; // �m�[�c�̃X�s�[�h�𐧌䂷�邽�߂̕ϐ�
    bool start; // �Q�[���J�n�t���O

    private void Start()
    {
        // GManager����m�[�c�̃X�s�[�h���擾
        NoteSpeed = GManagerReon.instance.noteSpeed;
    }

    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ��Q�[���J�n�t���O�𗧂Ă�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            start = true;
        }

        // �Q�[�����J�n���ꂽ��A�m�[�c���ړ�������
        if (start)
        {
            // �m�[�c�̈ʒu��O���ɃX�s�[�h�ɉ����Ĉړ�
            transform.position -= transform.forward * Time.deltaTime * NoteSpeed;
        }
    }
}