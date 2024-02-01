using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item tool;
    public Item matchedTool; // 资源可以被破坏的工具
    
    public Item dropItem; // 资源被破坏后掉落的物品
    public int minAmount = 2; // 资源掉落的最小数量
    public int maxAmount = 4; // 资源掉落的最大数量
    
    public int maxHealth = 20; // 资源的最大生命值
    private int currentHealth; // 当前生命值
    [SerializeField] GameObject healthBar; // 生命值条

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        healthBar = GameObject.Find("Canvas2/HealthBar");

        currentHealth = maxHealth; // 初始化当前生命值为最大生命值
        
        // healthBar.GetComponent<HealthBar>().SetMaxHealth(maxHealth); // 设置生命值条的最大值
        // Debug.Log(healthBar.GetComponent<HealthBar>().slider.value);
    }
    
    private void Update()
    {
        tool = ChooseSlot.heldItem;
    }

    void OnTriggerStay2D (Collider2D other)
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            if (other.CompareTag("Player") && tool == matchedTool)
            {
                HitResource(20);
            }
            else
            {
                Debug.Log("必須拿斧頭");
            }
        }
    }
    
    // 处理击打资源的操作
    public void HitResource(int damage)
    {
        currentHealth -= damage; // 减去造成的伤害
        Debug.Log(currentHealth);
        // healthBar.GetComponent<HealthBar>().SetHealth(currentHealth); // 更新生命值条
        
        // 检查资源是否已被破坏
        if (currentHealth <= 0)
        {
            DestroyResource();
        }
    }

    private void DestroyResource()
    {
        // 增加掉落物到背包
        int amount = Random.Range(minAmount, maxAmount + 1);
        inventoryManager.AddNewItem(dropItem, amount);
        
        Destroy(gameObject);
    }
}
