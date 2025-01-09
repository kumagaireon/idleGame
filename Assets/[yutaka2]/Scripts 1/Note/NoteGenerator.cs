using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    public static NoteGenerator instance;

    [Header("ノート")]
    [SerializeField] private GameObject notePrefab;

    // グループごとのノートリスト
    private List<List<GameObject>> groupList = new List<List<GameObject>>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }                
    }

   /// <summary>
   /// ノートを生成するメソッド
   /// </summary>
   /// <returns>生成されたノートオブジェクト<</returns>
    public GameObject GenerateNote()
    {
        return Instantiate(notePrefab);// ノートのプレハブをインスタンス化
    }
}
