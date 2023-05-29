using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresh : MonoBehaviour
{
    public Inventory inventory;
    public GameObject slotGrid;
    public GameObject emptySlot;
    public List<GameObject> slotsList = new List<GameObject>();
    
    // 清空物品槽的方法
    public void ClearSlots()
    {
        for (int i = 0; i < slotGrid.transform.childCount; i++)
        {
            if (slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(slotGrid.transform.GetChild(i).gameObject);
        }
        slotsList.Clear();
    }

    // 刷新背包和工具列的物品槽
    public void RefreshItem()
    {
        ClearSlots();

        for (int i = 0; i < inventory.itemList.Count; i++)
        {
            slotsList.Add(Instantiate(emptySlot));
            slotsList[i].transform.SetParent(slotGrid.transform);
            slotsList[i].GetComponent<Slot>().slotID = i;
            slotsList[i].GetComponent<Slot>().SetupSlot(inventory.itemList[i]);
        }
    }
}
