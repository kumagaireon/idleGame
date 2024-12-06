using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    public static NoteGenerator instance;

    [Header("�m�[�c")]
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
    /// //note�𐶐����郁�\�b�h
    /// </summary>
    /// <param name="num">���������</param>
    /// <param name="posNum">��������ʒu</param>
    public GameObject GenerateNote()
    {
        return Instantiate(notePrefab);        
    }
}
