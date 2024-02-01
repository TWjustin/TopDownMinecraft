using System;
using UnityEngine;

public class FarmingSystem : MonoBehaviour
{
    public InventoryManager inventoryManager;
    
    public Transform cropParent; // 作物的父物件，用於組織作物

    private bool isPlanted = false; // 是否已種植作物
    private int currentStage = 0; // 當前生長階段
    private float currentGrowthTime = 0f; // 當前成長時間

    public CropData currentCropData; // 當前種植的作物數據
    private GameObject currentCrop; // 當前種植的作物
    private bool harvestable = false; // 是否可收穫
    
    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    // 種植作物
    public void PlantCrop(CropData cropData)
    {
        // 檢查是否有其他作物正在種植中
        if (currentCrop != null)
        {
            Debug.Log("There is already a crop planted.");
            return;
        }

        // 根據作物數據創建新的作物預置物
        GameObject cropPrefab = cropData.growthStages[0];
        currentCrop = Instantiate(cropPrefab, transform.position, Quaternion.identity);
        currentCrop.transform.SetParent(cropParent);

        currentCropData = cropData;
        isPlanted = true;
        currentGrowthTime = 0f;
        currentStage = 0;
    }

    // 更新作物的成長狀態
    private void UpdateCropGrowth()
    {
        if (isPlanted)
        {
            currentGrowthTime += Time.deltaTime;

            // 判斷是否達到生長時間
            if (currentGrowthTime >= currentCropData.growthTimes[currentStage])
            {
                if(currentStage == currentCropData.growthStages.Length - 1)
                {
                    harvestable = true;
                }
                else
                {
                    // 作物進入下一個生長階段
                    currentStage++;
                }
                
                // 檢查是否達到最後一個生長階段
                if (currentStage < currentCropData.growthStages.Length)
                {
                    // 切換到下一個生長階段的預置物
                    Destroy(currentCrop);
                    currentCrop = Instantiate(currentCropData.growthStages[currentStage], transform.position, Quaternion.identity);
                    currentCrop.transform.SetParent(cropParent);

                    currentGrowthTime = 0f;
                }
            }
        }
    }

    // 收穫作物
    private void HarvestCrop()
    {
        if (isPlanted)
        {
            // 移除作物
            Destroy(currentCrop);

            isPlanted = false;
            currentStage = 0;
            currentGrowthTime = 0f;

            // 執行收穫後的操作，例如獲取獎勵
            inventoryManager.AddNewItem(currentCropData.cropItem, 1);
        }
    }

    private void Update()
    {
        // 更新作物的成長狀態
        UpdateCropGrowth();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(other.CompareTag("Player"))
            {
                PlantCrop(currentCropData);
            }
            else
            {
                Debug.Log("種不下去");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.O) && harvestable)
        {
            if(other.CompareTag("Player"))
            {
                Debug.Log("收穫");
                HarvestCrop();
            }
        }
    }
}
