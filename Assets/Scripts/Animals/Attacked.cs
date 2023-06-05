using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item dropItem;
    
    Renderer rend;
    Color originalColor;
    public float flashTime = 0.1f;
    
    public int maxHealth = 100;
    int currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        
        currentHealth = maxHealth;
        
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        
        // play hurt animation
        StartCoroutine(FlashRed());
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("Enemy died!");
        
        // die animation

        // disable the enemy
        GetComponent<Collider2D>().enabled = false;
        GetComponent<AnimalAI>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.enabled = false;

        Destroy(gameObject, 1f);
        inventoryManager.AddNewItem(dropItem, 2);
    }
    
    IEnumerator FlashRed()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(flashTime);
        rend.material.color = originalColor;
    }
}
