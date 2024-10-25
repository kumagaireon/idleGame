using UnityEngine;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour
{
    public Button[] buttons; // ボタンの配列
    public string[] videoNames; // 動画の名前の配列

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
           int index = i; // ラムダキャプチャ問題を回避するために必要
            buttons[i].onClick.AddListener(() => 
                FindObjectOfType<ButtonClick>().SwitchScene(videoNames[index])
            );
        }
    }
}
