using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSize = 5;
    public int equipExp = 50;
    public Dictionary<string, Equipment> slots;

    public Player player;
    public PlayerLevel playerExp;

    public InventoryUI ui;

    void Awake()
    {
        slots = new Dictionary<string, Equipment>(maxSize);
    }

    public void AddItem(Equipment item)
    {
        string key = item.getId();
        if (slots.Count < maxSize && slots.TryAdd(key, item))
        {
            ui.addToSlot(key, slots[key].getLevel(), slots[key].getUISprite());
            player.updateStats();
        }
        else
        {
            if (slots.ContainsKey(key))
            {
                slots[key].increaseLevel();
                ui.updateLevelTxt(key, slots[key].getLevel());
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

