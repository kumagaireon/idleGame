using System.Collections.Generic;
using UnityEngine;

namespace IdolGame.InGame.Entities;

public class Note
{
    // ノートの発生時間
    public float Time { get; set; }
    // ノートのグループの種類
    public int TypeOfGroup { get; set; }
    // ノートのグループに関する情報
    public int GroupInfo { get; set; }
    // ノートの位置情報（オプション）
    public List<Vector2>? Position { get; set; }
}