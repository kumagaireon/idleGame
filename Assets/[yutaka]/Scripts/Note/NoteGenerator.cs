using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    public static NoteGenerator instance;

    [Header("ノーツ")]
    [SerializeField] private GameObject notePrefab;

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
    /// //noteを生成するメソッド
    /// </summary>
    /// <param name="num">生成する個数</param>
    /// <param name="posNum">生成する位置</param>
    public GameObject GenerateNote()
    {
        return Instantiate(notePrefab);        
    }
}
