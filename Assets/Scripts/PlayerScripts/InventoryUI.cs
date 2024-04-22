using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public List<TextMeshProUGUI> slotTxt = new List<TextMeshProUGUI>();

    private Dictionary<string, GameObject> usedSlots;
    private Dictionary<string, TextMeshProUGUI> usedTxt;
    private int maxSize = 5;
    private int key;

    void Awake()
    {
        key = 0;
        usedSlots = new Dictionary<string, GameObject>(slots.Count);
        usedTxt = new Dictionary<string, TextMeshProUGUI>(slotTxt.Count);   
    }

    public void addToSlot(string id, float lvl, Sprite sp)
    {
        if (usedSlots.Count < maxSize && usedTxt.Count < maxSize)
        {
            slots[key].GetComponent<Image>().sprite = sp;
            usedSlots[id] = slots[key];

            slotTxt[key].text = lvl.ToString();
            usedTxt[id] = slotTxt[key];
            key++;
        }
    }

    public void updateLevelTxt(string id, float lvl)
    {
        usedTxt[id].text = lvl.ToString();
    }
}
