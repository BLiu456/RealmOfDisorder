using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : ItemObject
{
    public float level = 1f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Pick up into inventory logic goes here
            
            //After picking up, activate effect of equipment
            this.effect();
            Destroy(gameObject);
        }
    }

    public void increaseLevel()
    {
        level += 1f;
    }
}
