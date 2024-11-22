using UnityEngine;

namespace Kumagai
{
    public class TestSound : MonoBehaviour
    {
        void Start()
        {
            // SoundManagerを使用してBGMを再生
            SoundManager.Instance.PlayBgm("bgm_title", 1f);
        }

        void Update()
        {
            // Oキーが押されたときにBGMを再生
            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("押した");
                SoundManager.Instance.PlayBgm("bgm_title", 1f);
            }
        }
    }
}