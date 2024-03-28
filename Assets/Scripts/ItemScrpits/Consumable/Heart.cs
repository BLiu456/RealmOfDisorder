using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : ItemObject
{
    public GameObject heartUIPrefab;
    public override void OnTriggerEnter2D(Collider2D other)
    { 

    }
    public void Use(Health playerHealth)
    {
        int healAmount = playerHealth.getMaxHp() / 2;
        playerHealth.healed(healAmount);
    }
}
