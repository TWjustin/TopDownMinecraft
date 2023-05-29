using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Transform originalParent;
    public Inventory inventory;
    private int currentItemID;

    public GameObject selectedSlot;  // 已選擇的槽位
    private static Slot previousSelectedSlot;  // 先前選擇的格子
    public Color originalColor;
    public Item heldItem;   // 手持物品

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;  // slot
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false; // 使 Raycast 穿透
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        // Debug.Log(eventData.pointerCurrentRaycast.gameObject.name); // 顯示當前滑鼠所指的物件名稱
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name == "Image")
            {
                var parent = eventData.pointerCurrentRaycast.gameObject.transform.parent;
                transform.SetParent(parent.parent);    // 將物品放置在 slot中
                transform.position = parent.parent.position;
            
                var temp = inventory.itemList[currentItemID];
                inventory.itemList[currentItemID] = inventory.itemList[eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInParent<Slot>().slotID];
                inventory.itemList[eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInParent<Slot>().slotID] = temp;
            
                parent.position = originalParent.position; // 換回原本位置
                parent.SetParent(originalParent);
            
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

            if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)" || eventData.pointerCurrentRaycast.gameObject.name == "BackpackSlot(Clone)")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);  // 設到新 slot
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        
                inventory.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = inventory.itemList[currentItemID];
                if(eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID != currentItemID)
                    inventory.itemList[currentItemID] = null;
        
                GetComponent<CanvasGroup>().blocksRaycasts = true;  // 使 Raycast 不穿透
                return;
            }
        }
        
        // 其他任何位置都歸位
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
        // 檢測滑鼠左鍵是否點擊
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (eventData.pointerPress.transform.parent.CompareTag("Slot"))
            {
                // 選取該槽位
                selectedSlot = eventData.pointerPress.transform.parent.gameObject;
                
                if(previousSelectedSlot != null)
                    previousSelectedSlot.GetComponent<Image>().color = originalColor;
                selectedSlot.GetComponent<Image>().color = Color.white;
                previousSelectedSlot = selectedSlot.GetComponent<Slot>();
                
                // 手持
                heldItem = inventory.itemList[selectedSlot.GetComponent<Slot>().slotID];
                Debug.Log(heldItem.itemName);
            }
            else
            {
                Debug.Log("未選擇槽位");
            }
        }
    }
}
