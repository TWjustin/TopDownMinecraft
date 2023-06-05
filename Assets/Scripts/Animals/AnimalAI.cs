using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalAI : MonoBehaviour
{
    public float wanderRadius = 5f; // 漫遊半徑
    public float wanderTimer = 3f; // 漫遊時間

    private Vector2 target; // 目標位置
    private float timer; // 計時器

    private Rigidbody2D rb; // 剛體組件
    private AnimalSpawner animalSpawner;
    public float destroyRadius = 25f; // 銷毀半徑

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animalSpawner = FindObjectOfType<AnimalSpawner>();
        timer = wanderTimer;
        GetNewTarget();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            GetNewTarget();
            timer = wanderTimer;
        }
        MoveTowardsTarget();
        
        
        // 检查动物是否超出边界
        if (transform.position.x < animalSpawner.leftBound - destroyRadius || transform.position.x > animalSpawner.rightBound + destroyRadius ||
            transform.position.y < animalSpawner.bottomBound - destroyRadius || transform.position.y > animalSpawner.topBound + destroyRadius)
        {
            Destroy(gameObject);
            Debug.Log("Animal out of bounds");
        }
    }

    private void GetNewTarget()
    {
        // 隨機獲取一個新的目標位置
        target = (Vector2)transform.position + Random.insideUnitCircle * wanderRadius;
    }

    private void MoveTowardsTarget()
    {
        // 計算移動方向
        Vector2 direction = target - (Vector2)transform.position;

        // 將動物移動到目標位置
        rb.velocity = direction.normalized;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
