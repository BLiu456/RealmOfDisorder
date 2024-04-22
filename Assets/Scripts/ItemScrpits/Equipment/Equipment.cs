using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class Equipment : ItemObject
{
    protected string id; 
    public float level = 1f;
    public Sprite uiSprite;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Pick up into inventory logic goes here
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                Equipment item = clone();
                inventory.AddItem(item);
                other.GetComponent<Player>().popupMsg(popMsg);
            }
            Destroy(gameObject);
        }
    }

    public void increaseLevel()
    {
        level += 1f;
    }

    public string getId()
    {
        return id;
    }

    public Sprite getUISprite()
    {
        return uiSprite;
    }

    public float getLevel()
    {
        return level;
    }

    public abstract float getEffect();
    public abstract Equipment clone();
}
