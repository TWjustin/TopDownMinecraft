using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemOnMap : MonoBehaviour
{
    public Item thisItem;
    public InventoryManager inventoryManager;
    
    private void Start()
    { 
        // inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            InteractWithObjects();
        }
    }

    private void InteractWithObjects()
    {
        inventoryManager.AddNewItem(thisItem, 1);
        Destroy(gameObject);
    }

}
