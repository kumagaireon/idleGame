using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    // �Ȃ̃f�[�^��ۑ�����N���X
    public string name; // �Ȗ�
    public int maxBlock; // �ő�u���b�N��
    public int BPM; // �Ȃ�BPM�iBeats Per Minute�j
    public int offset; // �Ȃ̃I�t�Z�b�g
    public Note[] notes; // �m�[�c�̔z��
}

[Serializable]
public class Note
{
    // �m�[�c�f�[�^��ۑ�����N���X
    public int type; // �m�[�c�̎��
    public int num; // �m�[�c�̔ԍ�
    public int block; // �m�[�c��������u���b�N�ԍ�
    public int LPB; // ���߂�����̃m�[�c��
}
public class NotesManager : MonoBehaviour
{
    public int noteNum;//���m�[�c��
    private string songName;//�Ȗ�������ϐ�

    public List<int> LaneNum = new List<int>();//���Ԗڂ̃��[���Ƀm�[�c�������Ă��邩
    public List<int> NoteType = new List<int>();//���m�[�c��(�����O�m�[�c�Ƃ�)
    public List<float> NotesTime = new List<float>();//�m�[�c��������Əd�˂��Ă��鎞��
    public List<GameObject> NotesObj = new List<GameObject>();//�m�[�c�I�u�W�F�N�g

    [SerializeField] private float NotesSpeed;//�m�[�c�̃X�s�[�h

    [SerializeField] GameObject noteObj;//�m�[�c��Prefab�����
    [SerializeField] SongDataBaseReon dataBase; // �Ȃ̃f�[�^�x�[�X
    void OnEnable()
    {
        NotesSpeed = GManagerReon.instance.noteSpeed; // �m�[�c�̃X�s�[�h���擾
        noteNum = 0; // ���m�[�c����0�ɏ�����
        songName = dataBase.songData[GManagerReon.instance.songID].songName; // �v���C����Ȗ����擾
        Load(songName); // �ȃf�[�^�����[�h
    }

    private void Load(string SongName)
    {
        // Json�t�@�C����ǂݍ���
        string inputString = Resources.Load<TextAsset>(SongName).ToString(); // �Ȃ�Json�f�[�^��ǂݍ���
        Data inputJson = JsonUtility.FromJson<Data>(inputString); // Json�f�[�^���I�u�W�F�N�g�ɕϊ�
        noteNum = inputJson.notes.Length; // ���m�[�c����ݒ�
        GManagerReon.instance.maxScore = noteNum * 5; // �ő�X�R�A��ݒ�

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            // �ꏬ�߂̒������v�Z
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            // �m�[�c�Ԃ̒������v�Z
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            // �m�[�c�̍~���Ă��鎞�Ԃ��v�Z
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            NotesTime.Add(time); // �m�[�c�̎��Ԃ����X�g�ɒǉ�
            LaneNum.Add(inputJson.notes[i].block); // �m�[�c�̃u���b�N�ԍ������X�g�ɒǉ�
            NoteType.Add(inputJson.notes[i].type); // �m�[�c�̎�ނ����X�g�ɒǉ�
            float z = NotesTime[i] * NotesSpeed; // �m�[�c�̈ʒu���v�Z
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z), Quaternion.identity)); // �m�[�c�I�u�W�F�N�g�𐶐�
        }
    }
}