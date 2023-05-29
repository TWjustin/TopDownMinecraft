using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs; // 動物的預置物
    
    private Camera mainCamera;
    public float leftBound;
    public float rightBound;
    public float bottomBound;
    public float topBound;
    
    public float offsetX = 1f; // 生成位置的X軸偏移量
    public float offsetY = 1f; // 生成位置的Y軸偏移量

    public float maxSpawnSec = 10f; // 最大生成時間間隔

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    
    private void Start()
    {
        CalculateCameraBounds();
        
        // 啟動協程，控制生成動物的時間間隔
        StartCoroutine(SpawnAnimals());
    }

    private void Update()
    {
        CalculateCameraBounds();
    }

    public void CalculateCameraBounds()
    {
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        leftBound = mainCamera.transform.position.x - cameraWidth / 2f;
        rightBound = mainCamera.transform.position.x + cameraWidth / 2f;
        bottomBound = mainCamera.transform.position.y - cameraHeight / 2f;
        topBound = mainCamera.transform.position.y + cameraHeight / 2f;
    }

    private System.Collections.IEnumerator SpawnAnimals()
    {
        while (true)
        {
            // 隨機選擇一個動物預置物
            GameObject animalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];

            bool isLeft = Random.Range(0, 2) == 0; // 隨機選擇動物生成的位置是在左邊還是右邊
            float spawnX = isLeft ? Random.Range(leftBound - offsetX, leftBound) : Random.Range(rightBound, rightBound + offsetX);
            bool isBottom = Random.Range(0, 2) == 0; // 隨機選擇動物生成的位置是在下邊還是上邊            
            float spawnY = isBottom ? Random.Range(bottomBound - offsetY, bottomBound) : Random.Range(topBound, topBound + offsetY);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

            if (IsWaterArea(spawnPosition))
            {
                continue; // 如果是水域區域，跳過該位置
            }
            // 生成動物
            Instantiate(animalPrefab, spawnPosition, Quaternion.identity);

            // 等待一段時間再生成下一個動物
            yield return new WaitForSeconds(Random.Range(2f, maxSpawnSec));
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
}