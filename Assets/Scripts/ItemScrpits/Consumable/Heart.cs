using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : ItemObject
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health healthComp = other.GetComponent<Health>();

            if (healthComp != null)
            {
                int heal = healthComp.getMaxHp() / 2;
                healthComp.healed(heal);
            }
            
            Destroy(gameObject);
        }
    }
}
