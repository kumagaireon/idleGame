using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour
{
    // �ϐ��̐錾
    [SerializeField] private GameObject[] MessageObj; // �v���C���[�ɔ����`����Q�[���I�u�W�F�N�g�̔z��
    [SerializeField] NotesManager notesManager; // NotesManager�X�N���v�g���i�[����ϐ�
    [SerializeField] TextMeshProUGUI comboText; // �R���{����\������TextMeshProUGUI�I�u�W�F�N�g
    [SerializeField] TextMeshProUGUI scoreText; // �X�R�A��\������TextMeshProUGUI�I�u�W�F�N�g
    [SerializeField] GameObject finish; // �Q�[���I�����ɕ\������Q�[���I�u�W�F�N�g
    private new AudioSource audio; // �I�[�f�B�I�\�[�X�R���|�[�l���g
    [SerializeField] AudioClip hitSound; // �m�[�c��@�������̌��ʉ�
    float endTime = 0; // �Q�[���̏I������

    private void Start()
    {
        // �I�[�f�B�I�\�[�X�R���|�[�l���g���擾
        audio = GetComponent<AudioSource>();
        // �Ō�̃m�[�c�̎��Ԃ��擾���A�Q�[���̏I�����Ԃ�ݒ�
        endTime = notesManager.NotesTime[notesManager.NotesTime.Count - 1];
    }

    void Update()
    {
        // �Q�[�����J�n����Ă���ꍇ
        if (GManagerReon.instance.Start)
        {
            // �L�[���͂ɉ������m�[�c�̔���
            if (Input.GetKeyDown(KeyCode.D)) // "D"�L�[�������ꂽ�Ƃ�
            {
                if (notesManager.LaneNum[0] == 0) // ���[���̔ԍ�����v���邩�m�F
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManagerReon.instance.StartTime)), 0);
                }
                else if (notesManager.LaneNum[1] == 0)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManagerReon.instance.StartTime)), 1);
                }
            }

            if (Input.GetKeyDown(KeyCode.F)) // "F"�L�[�������ꂽ�Ƃ�
            {
                if (notesManager.LaneNum[0] == 1)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManagerReon.instance.StartTime)), 0);
                }
                else if (notesManager.LaneNum[1] == 1)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManagerReon.instance.StartTime)), 1);
                }
            }

            if (Input.GetKeyDown(KeyCode.J)) // "J"�L�[�������ꂽ�Ƃ�
            {
                if (notesManager.LaneNum[0] == 2)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManagerReon.instance.StartTime)), 0);
                }
                else if (notesManager.LaneNum[1] == 2)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManagerReon.instance.StartTime)), 1);
                }
            }

            if (Input.GetKeyDown(KeyCode.K)) // "K"�L�[�������ꂽ�Ƃ�
            {
                if (notesManager.LaneNum[0] == 3)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[0] + GManagerReon.instance.StartTime)), 0);
                }
                else if (notesManager.LaneNum[1] == 3)
                {
                    Judgement(GetABS(Time.time - (notesManager.NotesTime[1] + GManagerReon.instance.StartTime)), 1);
                }
            }

            // �Ō�̃m�[�c��@������̏���
            if (Time.time > endTime + GManagerReon.instance.StartTime)
            {
                finish.SetActive(true); // �Q�[���I���I�u�W�F�N�g��\��
                Invoke("ResuleScene", 3f); // 3�b��Ɍ��ʉ�ʂɑJ��
                return;
            }

            // �m�[�c��@���ׂ����Ԃ���0.2�b�o�߂��Ă����͂��Ȃ������ꍇ
            if (Time.time > notesManager.NotesTime[0] + 0.2f + GManagerReon.instance.StartTime)
            {
                message(3); // �~�X���b�Z�[�W��\��
                deleteData(0); // �m�[�c�f�[�^���폜
                Debug.Log("Miss"); // �f�o�b�O���O��"Miss"��\��
                GManagerReon.instance.miss++; // �~�X�J�E���g�𑝉�
                GManagerReon.instance.combo = 0; // �R���{�����Z�b�g
            }
        }
    }

    // �m�[�c�̔��菈��
    void Judgement(float timeLag, int numOffset)
    {
        audio.PlayOneShot(hitSound); // ���ʉ����Đ�
        if (timeLag <= 0.05) // ���莞�Ԃ�0.05�b�ȉ��̏ꍇ
        {
            Debug.Log("Perfect"); // �f�o�b�O���O��"Perfect"��\��
            message(0); // �p�[�t�F�N�g���b�Z�[�W��\��
            GManagerReon.instance.ratioScore += 5; // �X�R�A�����Z
            GManagerReon.instance.perfect++; // �p�[�t�F�N�g�J�E���g�𑝉�
            GManagerReon.instance.combo++; // �R���{�𑝉�
            deleteData(numOffset); // �m�[�c�f�[�^���폜
        }
        else if (timeLag <= 0.08) // ���莞�Ԃ�0.08�b�ȉ��̏ꍇ
        {
            Debug.Log("Great"); // �f�o�b�O���O��"Great"��\��
            message(1); // �O���[�g���b�Z�[�W��\��
            GManagerReon.instance.ratioScore += 3; // �X�R�A�����Z
            GManagerReon.instance.great++; // �O���[�g�J�E���g�𑝉�
            GManagerReon.instance.combo++; // �R���{�𑝉�
            deleteData(numOffset); // �m�[�c�f�[�^���폜
        }
        else if (timeLag <= 0.10) // ���莞�Ԃ�0.10�b�ȉ��̏ꍇ
        {
            Debug.Log("Bad"); // �f�o�b�O���O��"Bad"��\��
            message(2); // �o�b�h���b�Z�[�W��\��
            GManagerReon.instance.ratioScore += 1; // �X�R�A�����Z
            GManagerReon.instance.bad++; // �o�b�h�J�E���g�𑝉�
            GManagerReon.instance.combo = 0; // �R���{�����Z�b�g
            deleteData(numOffset); // �m�[�c�f�[�^���폜
        }
    }

    // �����̐�Βl��Ԃ��֐�
    float GetABS(float num)
    {
        if (num >= 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
    }

    // ���łɒ@�����m�[�c���폜����֐�
    void deleteData(int numOffset)
    {
        notesManager.NotesTime.RemoveAt(numOffset); // �m�[�c���ԃf�[�^���폜
        notesManager.LaneNum.RemoveAt(numOffset); // ���[���ԍ��f�[�^���폜
        notesManager.NoteType.RemoveAt(numOffset); // �m�[�c�^�C�v�f�[�^���폜
        GManagerReon.instance.score = (int)Math.Round(1000000 * Math.Floor(
            GManagerReon.instance.ratioScore / GManagerReon.instance.maxScore * 1000000) / 1000000); // �X�R�A���v�Z���čX�V
        comboText.text = GManagerReon.instance.combo.ToString(); // �R���{�����X�V
        scoreText.text = GManagerReon.instance.score.ToString(); // �X�R�A���X�V
    }

    // �����\������֐�
    void message(int judge)
    {
        Instantiate(MessageObj[judge],
            new Vector3(notesManager.LaneNum[0] - 1.5f, 0.76f, 0.15f),
            Quaternion.Euler(45, 0, 0));
    }

    // ���ʉ�ʂɑJ�ڂ���֐�
    private void ResuleScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}