using UnityEngine;

public class ItemOnMap : MonoBehaviour
{
    public Item[] dropItems;
    public InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (other.CompareTag("Player"))
            {
                InteractWithObjects();
            }
        }
    }

    private void InteractWithObjects()
    {
        inventoryManager.AddNewItem(dropItems[0], 1);
        if (dropItems.Length > 1)
        {
            if (Random.Range(0, 100) < 50)
            {
                inventoryManager.AddNewItem(dropItems[1], 1);
            }
        }
        
        Destroy(gameObject);
    }

}
