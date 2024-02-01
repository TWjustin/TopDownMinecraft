using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIntoFarm : MonoBehaviour
{
    public GameObject originalObject;  // 原始物体
    public GameObject FarmPrefab;       // 农田预制体
    public Item tool;

    private void Update()
    {
        tool = ChooseSlot.heldItem;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (other.CompareTag("Player") && tool.name == "Hoe")
            {
                Debug.Log(tool.name);
                TransformLand();
            }
        }
    }

    private void TransformLand()
    {
        Debug.Log("TransformLand");
        
        // 在此处编写点击地图时的逻辑操作
        Vector3 originalPosition = originalObject.transform.position;
        Destroy(originalObject);
        Instantiate(FarmPrefab, originalPosition, Quaternion.identity);
    }
}
