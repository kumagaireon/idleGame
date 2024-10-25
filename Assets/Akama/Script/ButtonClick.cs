using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public static string videoToPlay;

    public void SwitchScene(string videoName)
    {
        videoToPlay = videoName;
        SceneManager.LoadScene("Video Scene"); // Ÿ‚ÌƒV[ƒ“–¼‚ğw’è
    }
}
