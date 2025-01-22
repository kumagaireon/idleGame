using System.Collections.Generic;
using UnityEngine;

namespace IdolGame.InGame.Interfaces;

public interface INoteRepository
{
    // プールからノートを取得するメソッド
    GameObject GetPooledNote();
    
    // ノートをプールに返却するメソッド
    void ReturnPooledNote(GameObject note);
    
    // ノートをグループに追加するメソッド
    void AddToGroup(List<GameObject> notes);
    
    // グループからノートを取得するメソッド
    List<GameObject> GetGroup(int groupNum);
    
    // ノートを削除するメソッド
    void RemoveNote(GameObject note);
    
    // グループを削除するメソッド
    void RemoveGroup(int groupNum);
    
    // 全てのグループを取得するメソッド
    List<List<GameObject>> GetAllGroups();
}