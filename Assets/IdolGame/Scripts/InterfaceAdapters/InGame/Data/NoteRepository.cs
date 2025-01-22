using System.Collections.Generic;
using IdolGame.InGame.Interfaces;
using UnityEngine;

namespace IdolGame.InGame.Data;

public class NoteRepository : INoteRepository
{
    private readonly List<List<GameObject>> noteGroups = new List<List<GameObject>>();
    private readonly Queue<GameObject> notePool = new Queue<GameObject>();
    private readonly GameObject notePrefab;
    private readonly int initialPoolSize = 20;

    // コンストラクタでノートプレファブを受け取り、プールを初期化
    public NoteRepository(GameObject notePrefab)
    {
        this.notePrefab = notePrefab;
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject note = GameObject.Instantiate(this.notePrefab);
            note.SetActive(false);
            notePool.Enqueue(note);
        }
    }

    // プールからノートを取得するメソッド
    public GameObject GetPooledNote()
    {
        return notePool.Count > 0 ? notePool.Dequeue() : GameObject.Instantiate(notePrefab);
    }

    public void ReturnPooledNote(GameObject note)
    {
        note.SetActive(false);
        notePool.Enqueue(note);
    }

    public void AddToGroup(List<GameObject> notes)
    {
        noteGroups.Add(notes);
    }

    public List<GameObject> GetGroup(int groupNum)
    {
        return noteGroups[groupNum];
    }

    public void RemoveNote(GameObject note)
    {
        note.SetActive(false);
        notePool.Enqueue(note);
    }

    public void RemoveGroup(int groupNum)
    {
        noteGroups.RemoveAt(groupNum);
    }

    public List<List<GameObject>> GetAllGroups()
    {
        return noteGroups;
    }
}