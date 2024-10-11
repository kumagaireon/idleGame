using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�{�^���ł̃V�[���A�p�l���J�ڂ̃R���g���[���[
public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;

    public void PushTitleButton()
    {        
        SceneManager.LoadScene("TitleScene");
    }
    public void PushMenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void PushSelectButton()
    {
        SceneManager.LoadScene("SelectScene");
    }
    public void PushLiveButton()
    {
        SceneManager.LoadScene("LiveScene");
    }
    public void PushResultButton()
    {
        SceneManager.LoadScene("ResultScene");
    }
    public void PushSetLoveButton()
    {
        SceneManager.LoadScene("SetLoveScene");
    }
    public void PushOptionButton()
    {
        optionPanel.SetActive(!optionPanel.activeSelf);
    }
}
