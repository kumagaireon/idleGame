using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab; // 生成するオブジェクトのプレハブ
    public int gridSize = 10; // 縦横の生成数
    public float spacing = 1.0f; // オブジェクト間のスペース

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
