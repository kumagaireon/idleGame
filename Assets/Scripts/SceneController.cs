using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーン遷移を管理するコントローラー
public class SceneController : MonoBehaviour
{
    private void Update()
    {
        //Tキーでタイトル        
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("TitleScene");
        }
        //Mキーでゲーム        
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MenuScene");
        }
        //Sキーでゲーム        
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("SelectScene");
        }
        //Lキーでゲーム        
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("LiveScene");
        }
        //Lキーでゲーム        
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("LiveScene");
        }
    }
}
