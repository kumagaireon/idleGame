using System.Collections.Generic;
using UnityEngine;

namespace IdolGame.InGame.UI
{
    public class NoteView : MonoBehaviour
    {
        // ノートを表示するメソッド
        public void DisplayNotes(List<GameObject> notes)
        {
            foreach (var note in notes)
            {
                if (note != null)
                {
                    // ノートをアクティブにする
                    note.SetActive(true);
                    // ノートの位置やその他の表示に関する設定をここで行う
                    SpriteRenderer renderer = note.GetComponent<SpriteRenderer>();
                    // SpriteRendererを有効にする
                    renderer.enabled = true;
                }
            }
        }

        // ノートを非表示にするメソッド
        public void HideNotes(List<GameObject> notes)
        {
            foreach (var note in notes)
            {
                if (note != null)
                {
                    // ノートを非アクティブにする
                    note.SetActive(false);
                    SpriteRenderer renderer = note.GetComponent<SpriteRenderer>();
                    // SpriteRendererを無効にする
                    renderer.enabled = false;
                }
            }
        }

        // ノートの位置を更新するメソッド
        public void UpdateNotePosition(GameObject note, Vector2 newPosition)
        {
            if (note != null)
            {
                note.transform.position = newPosition;// ノートの位置を更新
            }
        }
    }
}