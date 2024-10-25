using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManagerReon : MonoBehaviour
{
    // �V���O���g���p�^�[�����g�p���āA���̃N���X�̃C���X�^���X����������݂���悤�ɂ���
    public static GManagerReon instance = null;

    // �Q�[���̃X�R�A�Ɋւ���ϐ�
    public float maxScore;     // �ő�X�R�A
    public float ratioScore;   // �X�R�A�̊���
    public int songID;         // �Ȃ�ID
    public float noteSpeed;    // �m�[�g�̑��x
    public bool Start;         // �Q�[�����J�n����Ă��邩�ǂ����̃t���O
    public float StartTime;    // �Q�[���J�n����
    public int combo;          // ���݂̃R���{��
    public int score;          // ���݂̃X�R�A
    public int perfect;        // �p�[�t�F�N�g�̐�
    public int great;          // �O���[�g�̐�
    public int bad;            // �o�b�h�̐�
    public int miss;           // �~�X�̐�

    public void Awake()
    {
        // �C���X�^���X�����݂��Ȃ��ꍇ�A���̃I�u�W�F�N�g���C���X�^���X�Ƃ��Đݒ肵�A�V�[���ԂŔj������Ȃ��悤�ɂ���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // ���łɃC���X�^���X�����݂���ꍇ�A���̃I�u�W�F�N�g��j������
            Destroy(this.gameObject);
        }
    }
}
