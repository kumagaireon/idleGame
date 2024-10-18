using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab; // ��������I�u�W�F�N�g�̃v���n�u
    public int gridSize = 10; // �c���̐�����
    public float spacing = 1.0f; // �I�u�W�F�N�g�Ԃ̃X�y�[�X

    void Start()
    {
        for (int x = -gridSize; x <= gridSize; x++)
        {
            for (int y = -gridSize; y <= gridSize; y++)
            {
                Vector3 spawnPosition = new Vector3(x * spacing, y * spacing, 0);
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
