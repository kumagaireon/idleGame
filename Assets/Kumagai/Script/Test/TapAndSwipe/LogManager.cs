using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    public static LogManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����ς���Ă��I�u�W�F�N�g��ێ�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LogMusicData(CSVAAAA.MusicData musicData)
    {
        Debug.Log($"����: {musicData.time}," +
            $"�p������: {musicData.keepTime}, " +
            $"����: {musicData.direction}," +
            $"�^�C�v: {musicData.type}");
    }
    public void LogKeepTimeEnd()
    {
        Debug.Log("�p�����Ԃ��I�����܂����B");
    }
}
