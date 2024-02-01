using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public Inventory backpack;
    public GameObject backpackPanel;
    public Inventory toolbar;
    public GameObject toolbarPanel;
    public TextMeshProUGUI itemInfo;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void OnEnable()
    {
        FinalRefresh();
        
        instance.itemInfo.text = "";
    }
    
    void FinalRefresh()
    {
        toolbarPanel.GetComponent<Refresh>().RefreshItem();
        backpackPanel.GetComponent<Refresh>().RefreshItem();
    }

    public static void UpdateItemInfo(string itemInfo)
    {
        instance.itemInfo.text = itemInfo;
    }
    
    public void AddNewItem(Item thisItem, int amount)
    {
        FinalRefresh();
        
        if (!toolbar.itemList.Contains(thisItem))
        {
            for (int i = 0; i < toolbar.itemList.Count; i++)
            {
                if(toolbar.itemList[i] == null)
                {
                    toolbar.itemList[i] = thisItem;
                    
                    break;
                }
                
            }
        }
        else if (toolbar.itemList.Contains(thisItem))
        {
            thisItem.itemHeld += amount;
        }
        else
        {
            if (!backpack.itemList.Contains(thisItem))
            {
                for (int i = 0; i < backpack.itemList.Count; i++)
                {
                    if(backpack.itemList[i] == null)
                    {
                        backpack.itemList[i] = thisItem;
                        break;
                    }
                }
            }
            else
            {
                thisItem.itemHeld += amount;
            }
        }
    }
    
    
}

