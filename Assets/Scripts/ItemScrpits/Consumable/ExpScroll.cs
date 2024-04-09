using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpScroll : ItemObject
{
    public int exp = 25;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerLevel lvl = other.GetComponent<PlayerLevel>();
            if (lvl != null)
            {
                effect(lvl);
                Destroy(gameObject);
            }
        }
    }

    public void effect(PlayerLevel lvl)
    {
        lvl.addExp(exp);
    }
}
