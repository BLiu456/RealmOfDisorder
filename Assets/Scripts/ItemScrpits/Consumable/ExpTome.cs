using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpTome : ItemObject
{
    public void Start()
    {
        popMsg = ""; //Display no text since it'll display the level up text
    }

    public override void effect(GameObject lvl)
    {
        lvl.GetComponent<PlayerLevel>().addExpToLvl();
    }
}
