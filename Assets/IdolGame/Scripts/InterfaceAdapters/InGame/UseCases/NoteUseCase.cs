
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Entities;
using IdolGame.InGame.Interfaces;
using UnityEngine;

namespace IdolGame.InGame.UseCases;

public class NoteUseCase
{
    private readonly INoteRepository _noteRepository;
    private readonly IPositionService _positionService;
    private readonly IAlphaService _alphaService;
    private readonly List<float> _groupSpawnTimes = new List<float>();
    public readonly float NoteLifeTime = 2.0f; // ノートの寿命時間

    public NoteUseCase(INoteRepository noteRepository, IPositionService positionService, IAlphaService alphaService)
    {
        _noteRepository = noteRepository;
        _positionService = positionService;
        _alphaService = alphaService;
    }

    public async UniTask GenerateNotes(Note note)
    {
        List<GameObject> notes = new List<GameObject>();
        for (int i = 0; i < note.InfoOfGroup; i++)
        {
            GameObject noteObj = _noteRepository.GetPooledNote();
            noteObj.SetActive(false);
            Vector2 position = _positionService.SetPosition(note.Position[i]);
            noteObj.transform.position = position;
            SpriteRenderer noteRenderer = noteObj.GetComponent<SpriteRenderer>();
            await _alphaService.FadeIn(noteRenderer);
            noteObj.SetActive(true);
            notes.Add(noteObj);
        }

        _noteRepository.AddToGroup(notes);
        _groupSpawnTimes.Add(Time.time);
    }

    public void DestroyNotes(int groupNum)
    {
        List<GameObject> noteGroup = _noteRepository.GetGroup(groupNum);
        foreach (var note in noteGroup)
        {
            if (note != null && note.activeSelf)
            {
                note.SetActive(false);
                _noteRepository.ReturnPooledNote(note);
            }
        }

        _noteRepository.RemoveGroup(groupNum);
    }

    public List<float> GetGroupSpawnTimes()
    {
        return _groupSpawnTimes;
    }

    public List<List<GameObject>> GetGroupList()
    {
        return _noteRepository.GetAllGroups();
    }

    public void RemoveGroupSpawnTime(int groupIndex)
    {
    }
}