using System.Collections.Generic;
using IdolGame.InGame.Interfaces;
using UnityEngine;

namespace IdolGame.InGame.Data;

public class NoteRepository : INoteRepository
{
    private readonly List<List<GameObject>> _noteGroups = new List<List<GameObject>>();
    private readonly Queue<GameObject> _notePool = new Queue<GameObject>();
    private readonly GameObject _notePrefab;
    private readonly int _initialPoolSize = 20;

    public NoteRepository(GameObject notePrefab)
    {
        _notePrefab = notePrefab;
        for (int i = 0; i < _initialPoolSize; i++)
        {
            GameObject note = GameObject.Instantiate(_notePrefab);
            note.SetActive(false);
            _notePool.Enqueue(note);
        }
    }

    public GameObject GetPooledNote()
    {
        if (_notePool.Count > 0)
        {
            return _notePool.Dequeue();
        }
        else
        {
            GameObject note = GameObject.Instantiate(_notePrefab);
            note.SetActive(false);
            return note;
        }
    }

    public void ReturnPooledNote(GameObject note)
    {
        note.SetActive(false);
        _notePool.Enqueue(note);
    }

    public void AddToGroup(List<GameObject> notes)
    {
        _noteGroups.Add(notes);
    }

    public List<GameObject> GetGroup(int groupNum)
    {
        return _noteGroups[groupNum];
    }

    public void RemoveNote(GameObject note)
    {
        note.SetActive(false);
        _notePool.Enqueue(note);
    }

    public void RemoveGroup(int groupNum)
    {
        List<GameObject> noteGroup = _noteGroups[groupNum];
        foreach (var note in noteGroup)
        {
            if (note != null)
            {
                RemoveNote(note);
            }
        }

        _noteGroups[groupNum] = null;
    }

    public List<List<GameObject>> GetAllGroups()
    {
        return _noteGroups;
    }
}