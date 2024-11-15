using UnityEngine;

namespace Kumagai
{
    public class PlaySoundExample : MonoBehaviour
    {
        private SoundController soundController;

        [SerializeField] private string soundName;
            
        void Start()
        {
            soundController = FindObjectOfType<SoundController>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                soundController.PlaySound(soundName);
            }
        }
    }
}