using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int baseHp = 10;
    public int effectiveHp = 10;
    public int basePower = 5;
    public int effectivePower = 5;
    public float atkCD; //Attack Cooldown

    [SerializeField]
    private Health hpManager;

    [SerializeField]
    private HealthUI hpUI;

    [SerializeField]
    private Shooting shoot;

    [SerializeField]
    private GameMaster gm;

    [SerializeField]
    private Inventory inv;

    void Start()
    {
        effectivePower = basePower;
        effectiveHp = baseHp;
        hpManager.setHealthValues(effectiveHp, effectiveHp);
        hpUI.changeBar();

        shoot.setAtkCD(atkCD);
        shoot.updatePower(effectivePower);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this.GetComponent<PlayerMovement>().getDashState() && this.GetComponent<Health>().isAlive)
        {
            if (collision.tag == "Enemy")
            {
                EnemyObject enemyComp = collision.GetComponent<EnemyObject>();
                hpManager.damaged(enemyComp.applyDamage());
                hpUI.changeBar();
            }
            else if (collision.tag == "Enemy_Atk")
            {
                Projectile projComp = collision.GetComponent<Projectile>();
                hpManager.damaged(projComp.applyDamage());
                hpUI.changeBar();
            }

            if (!this.GetComponent<Health>().isAlive)
            {
                gm.GetComponent<GameOver>().gameOver();
                this.GetComponent<PlayerMovement>().setSpeed(0);
                GameObject.FindGameObjectWithTag("RotatePoint").SetActive(false);
            }
        }
    }

    public void calcEffHp()
    {
        Equipment hlthEquip = inv.getEquipById("armor");
        float equipMod = 0;
        if (hlthEquip != null)
        {
            equipMod = hlthEquip.getEffect();
        }

        effectiveHp = (int)Math.Ceiling((float)baseHp * (1f + equipMod));
        hpManager.increaseMaxHp(effectiveHp);
        hpUI.changeBar();
    }

    public void calcEffPower()
    {
        Equipment swordEquip = inv.getEquipById("sword");
        float equipMod = 0;
        if (swordEquip != null)
        {
            equipMod = swordEquip.getEffect();
        }

        effectivePower = (int)Math.Ceiling((float)basePower * (1f + equipMod));
        shoot.updatePower(effectivePower);
    }

    public void levelStats(int level)
    {
        baseHp += 5 * level;
        basePower += 3 * level;

        calcEffHp();
        hpManager.healed(effectiveHp); //Full heal the player when leveling up
        hpUI.changeBar();
        calcEffPower();
    }

    //Mainly used when player obtains equipment
    public void updateStats()
    {
        calcEffHp();
        calcEffPower();
    }
}
