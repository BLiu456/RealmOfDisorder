using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSize = 5;
    public int equipExp = 10;
    public Dictionary<string, Equipment> slots;

    public Player player;
    public PlayerLevel playerExp;

    void Awake()
    {
        slots = new Dictionary<string, Equipment>(maxSize);
    }

    public void AddItem(Equipment item)
    {
        string key = item.getId();
        if (slots.Count < maxSize && slots.TryAdd(key, item))
        {
            player.updateStats();
        }
        else
        {
            if (slots.ContainsKey(key))
            {
                slots[key].increaseLevel();
                player.updateStats();
            }
            else
            {
                //If player inventory is full and equipment not in there.
                //Then convert into exp
                playerExp.addExp(equipExp);
            }
        }
    }

    public Equipment getEquipById(string id)
    {
        if (slots.ContainsKey(id))
        {
            return slots[id];
        }

        return null;
    }
}

