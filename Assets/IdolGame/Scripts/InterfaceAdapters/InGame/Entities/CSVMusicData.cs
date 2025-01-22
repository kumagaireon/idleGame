using System.Collections.Generic;
using UnityEngine;

namespace IdolGame.InGame.Entities;

public class CSVMusicData
{
    // ミュージックデータの時間
    public float Time { get; set; }
    // ミュージックデータのグループの種類
    public int TypeOfGroup { get; set; }
    // ミュージックデータのグループに関する情報
    public int InfoOfGroup { get; set; }
    // ミュージックデータの位置情報（オプション）
    public List<Vector2>? Position { get; set; }
}