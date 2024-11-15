using System;
using UnityEngine;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour
{
    public Button[] buttons; // �{�^���̔z��
    public string[] videoNames; // ����̖��O�̔z��

    [Obsolete("Obsolete")]
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
           int index = i; // �����_�L���v�`������������邽�߂ɕK�v
            buttons[i].onClick.AddListener(() => 
                FindObjectOfType<ButtonClick>().SwitchScene(videoNames[index])
            );
        }
    }
}
