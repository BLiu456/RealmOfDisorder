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
    public int pierceMod = 2;

    [SerializeField]
    private Player player;

    [SerializeField]
    private Shooting shoot;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI lvlText;
    [SerializeField]
    private Image expBar;

    public void levelUp()
    {
        level += 1;
        player.popupMsg("Level Up");
        lvlText.text = level.ToString();
        player.levelStats(level);

        switch (level)
        {
            case 5:
                shoot.setNumProj(3);
                shoot.setSpread(30f);
                break;
            case 25:
                shoot.setNumProj(5);
                shoot.setSpread(45f);
                break;
            default:
                if (level % 50 == 0)
                {
                    shoot.setBulletPierce(pierceMod++);
                }
                break;
        }
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

    public void addExpToLvl()
    {
        int amount = lvlRequire - exp;
        addExp(amount);
    }

    public void changeBar()
    {
        expBar.fillAmount = (float)exp / (float)lvlRequire;
    }
}
