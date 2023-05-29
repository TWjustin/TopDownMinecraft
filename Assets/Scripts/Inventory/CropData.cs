using UnityEngine;

[CreateAssetMenu(fileName = "New Crop Data", menuName = "Crop Data")]
public class CropData : ScriptableObject
{
    public string cropName; // 作物名稱
    public Item cropItem; // 作物物品
    public GameObject[] growthStages; // 作物的生長階段預置物
    public float[] growthTimes; // 每個生長階段的生長時間

    // 可以添加其他的作物相關數據，例如價格、收成等
}