using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �V�[���J�ڂ��Ǘ�����R���g���[���[
public class SceneController : MonoBehaviour
{
    private void Update()
    {
        //T�L�[�Ń^�C�g��        
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("TitleScene");
        }
        //M�L�[�ŃQ�[��        
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MenuScene");
        }
        //S�L�[�ŃQ�[��        
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("SelectScene");
        }
        //L�L�[�ŃQ�[��        
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("LiveScene");
        }
        //L�L�[�ŃQ�[��        
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("LiveScene");
        }
    }
}
