using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHP;
    public int maxHP;
    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = currentHP;
    }

    public void setHealthValues(int currHP, int maxHp)
    {
        this.currentHP = currHP;
        this.maxHP = maxHp;
    }

    public void damaged(int power)
    {
        currentHP -= power;

        if (currentHP <= 0)
        {
            isAlive = false;
        }
    }

    public void healed(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
}
