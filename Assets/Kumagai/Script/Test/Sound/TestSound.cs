using UnityEngine;

namespace Kumagai
{
    public class TestSound : MonoBehaviour
    {
        void Start()
        {
            SoundManager.Instance.PlayBgm("bgm_title", 1f);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("押した");
                SoundManager.Instance.PlayBgm("bgm_title", 1f);
            }
        }
    }
}