using UnityEngine;

namespace Kumagai
{
    public class PlaySoundExample : MonoBehaviour
    {
        // SoundControllerの参照を保持するフィールド
        [SerializeField] private SoundController soundController;

        // 再生するサウンドの名前を保持するフィールド
        [SerializeField] private string soundName;
            
        void Start()
        {
            // シーン内のSoundControllerを探して参照を取得
            soundController = FindObjectOfType<SoundController>();
        }

        private void Update()
        {
            // Oキーが押されたときにサウンドを再生
            if (Input.GetKeyDown(KeyCode.O))
            {
                soundController.PlaySound(soundName);
            }
        }
    }
}