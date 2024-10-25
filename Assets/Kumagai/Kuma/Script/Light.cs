using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] float Speed = 3;  // ノーツの消える速度
    [SerializeField] int Num = 0;  // レーン番号（どのキーが押されたかを判定するために使用）
    private Renderer Renderer;  // ノーツのレンダラーコンポーネント
    private float Alpha = 0;  // ノーツの透明度

    private void Awake()
    {
        // Rendererコンポーネントを取得
        Renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // ノーツの透明度が0より大きい場合、透明度を更新
        if (!(Renderer.material.color.a <= 0))
        {
            Renderer.material.color = new Color(
                Renderer.material.color.r,
                Renderer.material.color.g,
                Renderer.material.color.b,
                Alpha
            );
        }

        // Numの値に応じて特定のキーが押されたかどうかをチェック
        if (Num == 1)
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                ColoerChange();  // ノーツの透明度を変更
            }
        }
        if (Num == 2)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                ColoerChange();  // ノーツの透明度を変更
            }
        }
        if (Num == 3)
        {
            if (Input.GetKeyUp(KeyCode.J))
            {
                ColoerChange();  // ノーツの透明度を変更
            }
        }
        if (Num == 4)
        {
            if (Input.GetKeyUp(KeyCode.K))
            {
                ColoerChange();  // ノーツの透明度を変更
            }
        }

        // 毎フレームごとにAlpha値をSpeedに基づいて減少
        Alpha -= Speed * Time.deltaTime;
    }

    // ノーツの透明度を変更するメソッド
    void ColoerChange()
    {
        Alpha = 0.3f;  // 透明度を0.3に設定
        Renderer.material.color = new Color(
            Renderer.material.color.r,
            Renderer.material.color.g,
            Renderer.material.color.b,
            Alpha
        );
    }
}