using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    public List<GameObject> loot = new List<GameObject>();

    public GameObject selectLoot()
    {
        if (loot.Count > 0)
        {
            return loot[Random.Range(0, loot.Count)];
        }
        else
        {
            return null;
        }
    }
}
