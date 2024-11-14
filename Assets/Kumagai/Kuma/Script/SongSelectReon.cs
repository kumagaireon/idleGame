using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SongSelectReon : MonoBehaviour
{
    [SerializeField] SongDataBaseReon dataBase; // �Ȃ̃f�[�^�x�[�X
    [SerializeField] TextMeshProUGUI[] songNameText; // �Ȗ���\������TextMeshProUGUI�̔z��
    [SerializeField] TextMeshProUGUI[] songLevelText; // �ȃ��x����\������TextMeshProUGUI�̔z��
    [SerializeField] TextMeshProUGUI[] csvFileText; // �ȃ��x����\������TextMeshProUGUI�̔z��

    [SerializeField] Image songImage; // �Ȃ̉摜��\������Image�I�u�W�F�N�g

    AudioSource audio; // �I�[�f�B�I�\�[�X�R���|�[�l���g
    AudioClip Music; // ���ݑI������Ă���Ȃ̃I�[�f�B�I�N���b�v
    string songName; // ���ݑI������Ă���Ȃ̖��O
    int select; // ���ݑI������Ă���Ȃ̃C���f�b�N�X
  [SerializeField]  private string selectedVideoPath;
    public static string csvFileName { get; private set; }

    private void Start()
    {
        select = 0; // �ŏ��ɑI�������Ȃ��C���f�b�N�X0�ɐݒ�
        audio = GetComponent<AudioSource>(); // AudioSource�R���|�[�l���g���擾
        songName = dataBase.songData[select].songName; // �����̋Ȗ����f�[�^�x�[�X����擾
        selectedVideoPath = dataBase.songData[select].selectedVideoPath; // �����̋Ȗ����f�[�^�x�[�X����擾
        Music = (AudioClip)Resources.Load("Musics/" + songName); // �����̋Ȃ����\�[�X���烍�[�h
        SongUpdateALL(); // �ȏ���S�čX�V
    }

    void Update()
    {
        // �����L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (select < dataBase.songData.Length - 1) // �f�[�^�x�[�X�̒����ȓ��ł����
            {
                select++; // �Ȃ̑I�������̋ȂɈړ�
                SongUpdateALL(); // �ȏ���S�čX�V
            }
        }

        // ����L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (select > 0) // �C���f�b�N�X��0�ȏ�ł����
            {
                select--; // �Ȃ̑I����O�̋ȂɈړ�
                SongUpdateALL(); // �ȏ���S�čX�V
            }
        }

        // �X�y�[�X�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Space))
        {                        
            SongStart(); // �Ȃ̍Đ����J�n
        }
    }

    private void SongUpdateALL()
    {
        songName = dataBase.songData[select].songName; // ���ݑI������Ă���Ȗ����f�[�^�x�[�X����擾
        selectedVideoPath = dataBase.songData[select].selectedVideoPath; // ���ݑI������Ă���Ȗ����f�[�^�x�[�X����擾
        Music = (AudioClip)Resources.Load("Musics/" + songName); // ���ݑI������Ă���Ȃ����\�[�X���烍�[�h
        audio.Stop(); // ���ݍĐ����̉��y���~
        audio.PlayOneShot(Music); // �V�����Ȃ��Đ�

        for (int i = 0; i < 5; i++)
        {
            SongUpdate(i - 2); // �Ȃ̏����X�V
        }
    }

    private void SongUpdate(int id)
    {
        try
        {
            // �Ȗ��ƃ��x�����e�L�X�g�ɕ\��
            songNameText[id + 2].text = dataBase.songData[select + id].songName;
            songLevelText[id + 2].text = "Lv." + dataBase.songData[select + id].songLevel;
        }
        catch
        {
            // �C���f�b�N�X���͈͊O�̏ꍇ�A�e�L�X�g����ɂ���
            songNameText[id + 2].text = "";
            songLevelText[id + 2].text = "";
        }

        if (id == 0)
        {
            songImage.sprite = dataBase.songData[select + id].songImage; // �Ȃ̉摜��\��
        }
    }

    public void SongStart()
    {
        GManagerReon.instance.songID = select; // �I�����ꂽ�Ȃ�ID��GManager�ɐݒ�
        csvFileName = dataBase.songData[select].csvFileName;//�Ȃ̏����i�[
        SceneManager.LoadScene("LiveScene"); // ���y�V�[�������[�h
    }    
}