using System.Collections.Generic;
using UnityEngine;

namespace IdolGame.InGame.Presenters;

public static class NotePresenter
{
    public static void DisplayNotes(List<GameObject> notes)
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

    public static void HideNotes(List<GameObject> notes)
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

    public static void UpdateNotePosition(GameObject note, Vector2 newPosition)
    {
        if (note != null)
        {
            note.transform.position = newPosition;
        }
    }
}