using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpScroll : ItemObject
{
    public int exp = 25;

    public void Start()
    {
        popMsg = "EXP+";
    }

    public override void effect(GameObject lvl)
    {
        lvl.GetComponent<PlayerLevel>().addExp(exp);
    }
}
