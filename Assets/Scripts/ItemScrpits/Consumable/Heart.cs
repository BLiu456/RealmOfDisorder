using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : ItemObject
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health hlth = other.GetComponent<Health>();
            HealthUI hlthUI = other.GetComponent<HealthUI>();
            if (hlth != null)
            {
                effect(hlth);
                hlthUI.changeBar();
                Destroy(gameObject);
            }
        }
    }
    public void effect(Health playerHealth)
    {
        int healAmount = playerHealth.getMaxHp() / 2;
        playerHealth.healed(healAmount);
    }
}
