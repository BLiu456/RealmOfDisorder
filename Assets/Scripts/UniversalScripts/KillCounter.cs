using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public TextMeshProUGUI killtxt;
    public int killCount = 0;

    public void increment()
    {
        killCount++;
        killtxt.text = killCount.ToString();
    }
}
