using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;    // �X�R�A��\�����邽�߂�TextMeshProUGUI�I�u�W�F�N�g
    [SerializeField] TextMeshProUGUI perfectText;  // �p�[�t�F�N�g�̐���\�����邽�߂�TextMeshProUGUI�I�u�W�F�N�g
    [SerializeField] TextMeshProUGUI greatText;    // �O���[�g�̐���\�����邽�߂�TextMeshProUGUI�I�u�W�F�N�g
    [SerializeField] TextMeshProUGUI badText;      // �o�b�h�̐���\�����邽�߂�TextMeshProUGUI�I�u�W�F�N�g
    [SerializeField] TextMeshProUGUI missText;     // �~�X�̐���\�����邽�߂�TextMeshProUGUI�I�u�W�F�N�g

    // �L���ɂȂ����Ƃ��ɌĂяo����郁�\�b�h
    private void OnEnable()
    {
        // GManager����e�X�R�A�����擾���ăe�L�X�g�ɔ��f
        scoreText.text = GManagerReon.instance.score.ToString();
        perfectText.text = GManagerReon.instance.perfect.ToString();
        greatText.text = GManagerReon.instance.great.ToString();
        badText.text = GManagerReon.instance.bad.ToString();
        missText.text = GManagerReon.instance.miss.ToString();
    }

    // ���g���C�{�^���������ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    public void Retry()
    {
        // GManager�̃X�R�A�������Z�b�g
        GManagerReon.instance.perfect = 0;
        GManagerReon.instance.great = 0;
        GManagerReon.instance.bad = 0;
        GManagerReon.instance.miss = 0;
        GManagerReon.instance.maxScore = 0;
        GManagerReon.instance.ratioScore = 0;
        GManagerReon.instance.score = 0;
        GManagerReon.instance.combo = 0;

        // "MusicScene" �V�[�����ă��[�h
        SceneManager.LoadScene("MusicScene");
    }
}
