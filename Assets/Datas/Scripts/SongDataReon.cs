using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject ���쐬���邽�߂̃��j���[���ڂ�ǉ�

[CreateAssetMenu(fileName ="SongData",menuName ="�y�ȃf�[�^���쐬")]
public class SongDataReon : ScriptableObject
{
    // �y�ȃf�[�^�̃v���p�e�B
    public string songID;     // �y�Ȃ�ID
    public string songName;   // �y�Ȃ̖��O
    public int songLevel;     // �y�Ȃ̓�Փx���x��
    public Sprite songImage;  // �y�Ȃ̉摜
}
