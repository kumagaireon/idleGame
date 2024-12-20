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
        if (notePrefab == null)
        {
            Debug.LogError("Note prefab is not assigned in NoteGenerator!");
            return null;
        }

        GameObject note = Instantiate(notePrefab);
        if (note == null)
        {
            Debug.LogError("Failed to instantiate note prefab");
            return null;
        }

        Debug.Log($"Successfully generated note: {note.name}");
        return note;
    }
}
