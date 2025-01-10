using System.Collections.Generic;
using UnityEngine;

namespace IdolGame.InGame.UI
{
    public class NoteView : MonoBehaviour
    {
        public void DisplayNotes(List<GameObject> notes)
        {
            foreach (var note in notes)
            {
                if (note != null)
                {
                    note.SetActive(true);
                    // ノートの位置やその他の表示に関する設定をここで行う
                    SpriteRenderer renderer = note.GetComponent<SpriteRenderer>();
                    renderer.enabled = true;
                }
            }
        }

        public void HideNotes(List<GameObject> notes)
        {
            foreach (var note in notes)
            {
                if (note != null)
                {
                    note.SetActive(false);
                    SpriteRenderer renderer = note.GetComponent<SpriteRenderer>();
                    renderer.enabled = false;
                }
            }
        }

        public void UpdateNotePosition(GameObject note, Vector2 newPosition)
        {
            if (note != null)
            {
                note.transform.position = newPosition;
            }
        }
    }
}