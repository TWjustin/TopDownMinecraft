using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIntoFarm : MonoBehaviour
{
    public GameObject originalObject;  // 原始物体
    public GameObject FarmPrefab;       // 农田预制体
    private ItemOnDrag itemOnDrag;

    private void Start()
    {
        // itemOnDrag = FindObjectOfType<ItemOnDrag>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (other.CompareTag("Player"))
            {
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
