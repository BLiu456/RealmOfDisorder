using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : ItemObject
{
    void Start()
    {
        popMsg = "50% HEAL";    
    }

    public override void effect(GameObject other)
    {
        Health hlth = other.GetComponent<Health>();
        HealthUI hlthUI = other.GetComponent<HealthUI>();
        if (hlth != null)
        {
            int healAmount = hlth.getMaxHp() / 2;
            hlth.healed(healAmount);
            hlthUI.changeBar();
        }
    }
}
