using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSlot : MonoBehaviour
{
    public Inventory inventory;
    private int currentItemID;
    
    private GameObject selectedSlot;  // 已選擇的槽位
    private Slot previousSelectedSlot;  // 先前選擇的格子
    public Color originalColor;
    public static Item heldItem;   // 手持物品
    
    // Start is called before the first frame update
    void Start()
    {
        // 一開始選第一格
        selectedSlot = GameObject.Find("Slot(Clone)");
        Choose();
    }

    // Update is called once per frame
    void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta > 0f)
        {
            // 向上滾動，切換到下一個物品
            SwitchToNextItem();
        }
        else if (scrollDelta < 0f)
        {
            // 向下滾動，切換到上一個物品
            SwitchToPreviousItem();
        }
    }

    private void SwitchToPreviousItem()
    {
        currentItemID = selectedSlot.GetComponent<Slot>().slotID;
        
        currentItemID++;
        if (currentItemID >= inventory.itemList.Count)
        {
            currentItemID = 0;
        }

        // 執行切換物品欄邏輯，例如更新 UI 或設置選中的物品狀態
        Debug.Log(currentItemID);
        Choose();
    }

    private void SwitchToNextItem()
    {
        currentItemID = selectedSlot.GetComponent<Slot>().slotID;
        
        currentItemID--;
        if (currentItemID < 0)
        {
            currentItemID = inventory.itemList.Count - 1;
        }

        // 執行切換物品欄邏輯，例如更新 UI 或設置選中的物品狀態
        Debug.Log(currentItemID);
        Choose();
    }
    
    public void Choose()
    {
        selectedSlot = Refresh.slotsList[currentItemID];
        
        if(previousSelectedSlot != null)
            previousSelectedSlot.GetComponent<Image>().color = originalColor;
        selectedSlot.GetComponent<Image>().color = Color.white;
        previousSelectedSlot = selectedSlot.GetComponent<Slot>();
        
        // 手持
        heldItem = inventory.itemList[selectedSlot.GetComponent<Slot>().slotID];
        Debug.Log(heldItem.itemName);
    }
}
