using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;  // 物体预制体
    public Vector2Int xRange;  // x轴范围
    public Vector2Int yRange;  // y轴范围
    public int spawnCount;  // 生成数量

    public GameObject container;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomObject();
    }
    
    Vector2 GetRandomPosition()
    {
        int x = Random.Range(xRange.x, xRange.y);
        int y = Random.Range(yRange.x, yRange.y);

        return new Vector2(x, y);
    }
    
    void SpawnRandomObject()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 position = GetRandomPosition();
            // 檢查位置是否為水域區域
            if (IsWaterArea(position))
            {
                continue; // 如果是水域區域，跳過該位置
            }
            if (HasObject(position))
            {
                continue;
            }
            int index = Random.Range(0, objectsToSpawn.Length);
            Instantiate(objectsToSpawn[index], position, Quaternion.identity, container.transform);
        }
    }
    
    bool IsWaterArea(Vector2 position)
    {
        // 檢查該位置是否為水域區域
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Water"))
            {
                return true;
            }
        }
        return false;
    }
    
    bool HasObject(Vector2 position)
    {
        // 檢查該位置是否已有物件
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Item"))
            {
                return true;
            }
        }
        return false;
    }
}
