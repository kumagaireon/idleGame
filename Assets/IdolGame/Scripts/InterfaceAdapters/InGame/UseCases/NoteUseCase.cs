
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using IdolGame.InGame.Entities;
using IdolGame.InGame.Interfaces;
using UnityEngine;

namespace IdolGame.InGame.UseCases;

public class NoteUseCase
{
    private readonly INoteRepository noteRepository; private readonly IPositionService positionService; private readonly IAlphaService alphaService; private readonly List<float> groupSpawnTimes = new List<float>(); public readonly float NoteLifeTime = 2.0f;

    // コンストラクタでINoteRepository、IPositionService、IAlphaServiceを受け取る
    public NoteUseCase(INoteRepository noteRepository, IPositionService positionService, IAlphaService alphaService)
    {
        this.noteRepository = noteRepository;
        this.positionService = positionService;
        this.alphaService = alphaService;
    }

    // ノートを生成するメソッド
    public async UniTask GenerateNotes(Note note)
    {
        List<GameObject> notes = new List<GameObject>();
        for (int i = 0; i < note.GroupInfo; i++)
        {
            GameObject noteObj = noteRepository.GetPooledNote();
            noteObj.SetActive(false);
            Vector2 position = positionService.GetPosition(note.Position[i]);
            noteObj.transform.position = position;
            SpriteRenderer noteRenderer = noteObj.GetComponent<SpriteRenderer>();
            await alphaService.FadeIn(noteRenderer); // ノートのフェードイン

            noteObj.SetActive(true);
            notes.Add(noteObj);
        }

        noteRepository.AddToGroup(notes); // ノートグループに追加
        groupSpawnTimes.Add(Time.time); // ノートの生成時間を記録
    }

    // ノートグループを破棄するメソッド
    public void DestroyNotes(int groupNum)
    {
        List<GameObject> noteGroup = noteRepository.GetGroup(groupNum);
        noteRepository.RemoveGroup(groupNum); // ノートグループを削除
        noteGroup.RemoveAll(note => note != null && note.activeSelf); // ノートをプールに返却 
    }

    // ノートグループの生成時間を取得するメソッド
    public List<float> GetGroupSpawnTimes()
    {
        return groupSpawnTimes;
    }

    // ノートグループのリストを取得するメソッド
    public List<List<GameObject>> GetGroupList()
    {
        return noteRepository.GetAllGroups();
    }

    // ノートグループの生成時間を削除するメソッド（未実装）
    public void RemoveGroupSpawnTime(int groupIndex)
    {
        if (groupIndex >= 0 && groupIndex < groupSpawnTimes.Count)
        {
            groupSpawnTimes.RemoveAt(groupIndex);
        }
    }
}