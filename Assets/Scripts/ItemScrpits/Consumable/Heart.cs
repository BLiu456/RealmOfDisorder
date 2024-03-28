using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : ItemObject
{
    public GameObject heartUIPrefab;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null && inventory.AddItem(heartUIPrefab)) 
            {
                Destroy(gameObject); 
            }
        }


    }
    public void Use(Health playerHealth)
    {
        int healAmount = playerHealth.getMaxHp() / 2;
        playerHealth.healed(healAmount);
    }
}
