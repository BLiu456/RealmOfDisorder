using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    public int level = 1;
    public int lvlRequire = 50;
    public int exp = 0;

    [SerializeField]
    private Player player;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI lvlText;
    [SerializeField]
    private Image expBar;

    public void levelUp()
    {
        level += 1;
        lvlText.text = level.ToString();
        player.levelStats(level);
    }

    public int getLvl()
    {
        return level;
    }

    public void increaseRequire()
    {
        lvlRequire = 50 * level; 
    }

    public void addExp(int amount)
    {
        exp += amount;

        if (exp >= lvlRequire)
        {
            levelUp();

            int carryOver = exp - lvlRequire;

            increaseRequire();
            exp = carryOver;
        }

        changeBar();
    }

    public void changeBar()
    {
        expBar.fillAmount = (float)exp / (float)lvlRequire;
    }
}
