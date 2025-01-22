using System.Collections.Generic;
using UnityEngine;

namespace IdolGame.InGame.Presenters;

public static class NotePresenter
{
    // ノートを表示するメソッド
    public static void DisplayNotes(List<GameObject> notes)
    {
        foreach (var note in notes)
        {
            if (note != null)
            {
                note.SetActive(true);
                SpriteRenderer renderer = note.GetComponent<SpriteRenderer>();
                renderer.enabled = true;
            }
        }
    }

    // ノートを非表示にするメソッド
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

    // ノートの位置を更新するメソッド
    public static void UpdateNotePosition(GameObject note, Vector2 newPosition)
    {
        if (note != null)
        {
            note.transform.position = newPosition;
        }
    }
}