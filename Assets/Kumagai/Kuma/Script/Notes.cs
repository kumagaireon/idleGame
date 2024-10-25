using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] float NoteSpeed; // ノーツのスピードを制御するための変数
    bool start; // ゲーム開始フラグ

    private void Start()
    {
        // GManagerからノーツのスピードを取得
        NoteSpeed = GManagerReon.instance.noteSpeed;
    }

    void Update()
    {
        // スペースキーが押されたらゲーム開始フラグを立てる
        if (Input.GetKeyDown(KeyCode.Space))
        {
            start = true;
        }

        // ゲームが開始されたら、ノーツを移動させる
        if (start)
        {
            // ノーツの位置を前方にスピードに応じて移動
            transform.position -= transform.forward * Time.deltaTime * NoteSpeed;
        }
    }
}