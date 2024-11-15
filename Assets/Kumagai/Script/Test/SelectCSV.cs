using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectCSV : MonoBehaviour
{
    [SerializeField] SongDataBaseReon dataBase; // �Ȃ̃f�[�^�x�[�X
    [SerializeField] string songName; // ���ݑI������Ă���Ȃ̖��O
    int select; // ���ݑI������Ă���Ȃ̃C���f�b�N�X
    [SerializeField] public static string csvFileName; // CSV�t�@�C����

    private void Start()
    {
        select = 0;
        songName = dataBase.songData[select].songName;
        csvFileName = dataBase.songData[select].csvFileName;
        SongUpdateALL();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (select < dataBase.songData.Length - 1)
            {
                select++;
                SongUpdateALL();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (select > 0)
            {
                select--;
                SongUpdateALL();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SongStart();
        }
    }

    private void SongUpdateALL()
    {
        songName = dataBase.songData[select].songName;
        csvFileName = dataBase.songData[select].csvFileName;
    }

    public void SongStart()
    {
        CSVPlayer.csvFileName = dataBase.songData[select].csvFileName;
        //CSVAAAA.Instance.csvAAAA(); // CSV�f�[�^���ēǂݍ���
        CSVPlayer.Instance.StartPlayback(); // �Đ��J�n
        Debug.Log(csvFileName);
    }
}