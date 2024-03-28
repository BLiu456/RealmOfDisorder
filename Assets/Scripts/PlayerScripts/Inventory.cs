using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots; 

   
    public bool AddItem(Equipment item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount == 0)
            {
                Instantiate(item, slots[i].transform, false);
                return true; 
            }
        }
        return false;
    }
}

