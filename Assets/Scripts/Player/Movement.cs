using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    private Vector3 moveDirection;  // 角色方向
    
    public int gridSize = 1; // 格子大小
    public Vector2Int currentPlayerPosition; // 玩家當前位置
    public GameObject hoverEffect;
    
    public GameObject backpack;
    bool isOpen;
    

    private void Start()
    {
        // 初始化玩家位置
        currentPlayerPosition = GetPlayerGridPosition();
        // 實例化 hover 效果的遊戲物件並設定為禁用狀態
        hoverEffect = Instantiate(hoverEffect);
        hoverEffect.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        // Get the horizontal and vertical inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        // 更新玩家位置
        currentPlayerPosition = GetPlayerGridPosition();
        // 檢查前一格的位置
        Vector2Int previousCellPosition = GetPreviousCellPosition();
        // 獲取前一格的世界座標
        Vector3 previousCellWorldPos = new Vector3(previousCellPosition.x * gridSize, previousCellPosition.y * gridSize, 0f);
        // 激活或禁用 hover 效果的遊戲物件
        hoverEffect.SetActive(true); // 激活 hover 效果物件
        hoverEffect.transform.position = previousCellWorldPos; // 將 hover 效果物件移動到前一格的位置
        
        OpenBackpack();
    }
    
    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    
    Vector2Int GetPlayerGridPosition()
    {
        // 根據玩家的世界座標計算格子座標
        Vector2Int gridPosition = new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.y / gridSize)
        );

        return gridPosition;
    }
    
    Vector2Int GetPreviousCellPosition()
    {
        // 根據玩家的前進方向計算前一格的位置
        Vector2Int forwardDirection = GetCurrentForwardDirection();
        Vector2Int previousCellPosition = currentPlayerPosition + forwardDirection;

        return previousCellPosition;
    }

    Vector2Int GetCurrentForwardDirection()
    {
        // 根據玩家的朝向計算前進方向
        Vector2Int forwardDirection = Vector2Int.zero;

        // 偵測玩家的輸入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 根據輸入計算前進方向
        if (horizontalInput > 0)
        {
            forwardDirection = Vector2Int.right;
        }
        else if (horizontalInput < 0)
        {
            forwardDirection = Vector2Int.left;
        }
        else if (verticalInput > 0)
        {
            forwardDirection = Vector2Int.up;
        }
        else if (verticalInput < 0)
        {
            forwardDirection = Vector2Int.down;
        }

        return forwardDirection;
    }

    void OpenBackpack()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            backpack.SetActive(isOpen);
        }
    }
}
