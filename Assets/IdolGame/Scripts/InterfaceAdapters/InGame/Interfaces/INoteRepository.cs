using System.Collections.Generic;
using UnityEngine;

namespace IdolGame.InGame.Interfaces;

public interface INoteRepository
{
    GameObject GetPooledNote();
    void ReturnPooledNote(GameObject note);
    void AddToGroup(List<GameObject> notes);
    List<GameObject> GetGroup(int groupNum);
    void RemoveNote(GameObject note);
    void RemoveGroup(int groupNum);
    List<List<GameObject>> GetAllGroups();
}