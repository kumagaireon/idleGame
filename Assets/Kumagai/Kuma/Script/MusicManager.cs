using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] SongDataBaseReon dataBase; // �Ȃ̃f�[�^�x�[�X
    AudioSource audio; // �I�[�f�B�I�\�[�X�R���|�[�l���g
    AudioClip Music; // ���ݑI������Ă���Ȃ̃I�[�f�B�I�N���b�v
    string songName; // ���ݑI������Ă���Ȃ̖��O
    bool played; // �Ȃ��Đ����ꂽ���ǂ����������t���O

    private void Start()
    {
        // ���ݑI������Ă���Ȃ̖��O���f�[�^�x�[�X����擾
        songName = dataBase.songData[GManagerReon.instance.songID].songName; //�ύX�_
        audio = GetComponent<AudioSource>(); // AudioSource�R���|�[�l���g���擾
        Music = GetComponent<AudioClip>(); // ���ݑI������Ă���Ȃ̃I�[�f�B�I�N���b�v���擾
        played = false; // �Đ��t���O��������
    }

    private void Update()
    {
        // �X�y�[�X�L�[��������A�܂��Ȃ��Đ�����Ă��Ȃ��ꍇ
        if ((Input.GetKeyUp(KeyCode.Space)) && !played)
        {
            GManagerReon.instance.Start = this; // GManager�ł��̃C���X�^���X���J�n�t���O�Ƃ��Đݒ�
            GManagerReon.instance.StartTime = Time.time; // �Q�[���J�n���Ԃ�ݒ�
            played = true; // �Đ��t���O��ݒ�
            audio.PlayOneShot(Music); // �Ȃ��Đ�
        }

    }
}
