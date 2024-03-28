using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : ItemObject
{
    public float level = 1f;


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Pick up into inventory logic goes here
            Inventory inventory = other.GetComponent<Inventory>(); 
            if (inventory != null && inventory.AddItem(this))
            {
                
                this.effect();
                Destroy(gameObject);
            }

        }
    }

    public void increaseLevel()
    {
        level += 1f;
    }
}
