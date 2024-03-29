using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Equipment
{
    public float buffAmount = 0f;
    public float buffModifier = 0.1f;

    public override void effect()
    {
        buffAmount = buffModifier * level;
    }

    public float getBuff()
    {
        return buffAmount;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null && inventory.AddItem(this))
            {

                this.effect();
                Destroy(gameObject);
            }

        }
    }

}
