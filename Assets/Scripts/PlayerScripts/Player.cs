using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int baseHp = 10;
    public int effectiveHp = 10;
    public int basePower = 5;
    public int effectivePower = 5;
    public float baseSpd = 6f;
    public float effectiveSpd = 6f;
    public float baseStamina = 30f;
    public float effectiveStamina = 30f;
    public float baseAtkCD = 1f; //Attack Cooldown
    public float effectiveAtkCD = 1f;
    private bool invulOn = false;

    [Header("Managers")]
    [SerializeField]
    private Health hpManager;

    [SerializeField]
    private HealthUI hpUI;

    [SerializeField]
    private PlayerMovement move;

    [SerializeField]
    private Shooting shoot;

    [SerializeField]
    private GameMaster gm;

    [SerializeField]
    private Inventory inv;

    [SerializeField]
    private PopupGenerator pop;

    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        invulOn = false;
        effectivePower = basePower;
        effectiveHp = baseHp;
        effectiveSpd = baseSpd;
        effectiveStamina = baseStamina;
        effectiveAtkCD = baseAtkCD;
        hpManager.setHealthValues(effectiveHp, effectiveHp);
        hpUI.changeBar();

        move.setSpeed(effectiveSpd);
        move.setMaxStamina(effectiveStamina);
        shoot.setAtkCD(effectiveAtkCD);
        shoot.updatePower(effectivePower);

       
    }

    private void OnTriggerEnter2D(Collider2D collision) //For single instance projectile attacks
    {
        if (!invulOn && !this.GetComponent<PlayerMovement>().getDashState())
        {
            if (collision.tag == "Enemy_Atk")
            {
                audioSource.Play();
                Debug.Log("hit");
                Projectile projComp = collision.GetComponent<Projectile>();
                hpManager.damaged(projComp.applyDamage());
                StartCoroutine(activateIFrame());
                hpUI.changeBar();
            }

            if (!this.GetComponent<Health>().isAlive)
            {
                gm.GetComponent<GameOver>().gameOver();
                move.setSpeed(0);
                GameObject.FindGameObjectWithTag("RotatePoint").SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //For continuous enemy attacks
    {
        if (!invulOn && !this.GetComponent<PlayerMovement>().getDashState())
        {
            if (collision.tag == "Enemy")
            {
                audioSource.Play();
                EnemyObject enemyComp = collision.GetComponent<EnemyObject>();
                hpManager.damaged(enemyComp.applyDamage());
                StartCoroutine(activateIFrame());
                hpUI.changeBar();
            }
            if (collision.tag == "Enemy_Atk")
            {
                audioSource.Play();
                Debug.Log("hit");
                Projectile projComp = collision.GetComponent<Projectile>();
                hpManager.damaged(projComp.applyDamage());
                StartCoroutine(activateIFrame());
                hpUI.changeBar();
            }

            if (!this.GetComponent<Health>().isAlive)
            {
                gm.GetComponent<GameOver>().gameOver();
                move.setSpeed(0);
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

    public void calcSpeed()
    {
        Equipment spdEquip = inv.getEquipById("boots");
        float equipMod = 0;
        if (spdEquip != null)
        {
            equipMod = spdEquip.getEffect();
        }

        effectiveSpd = baseSpd + equipMod;
        move.setSpeed(effectiveSpd);
    }

    public void calcStamina()
    {
        Equipment stamEquip = inv.getEquipById("necklace");
        float equipMod = 0;
        if (stamEquip != null)
        {
            equipMod = stamEquip.getEffect();
        }

        effectiveStamina = baseStamina + equipMod;
        move.setMaxStamina(effectiveStamina);
    }

    public void calcAtkCD()
    {
        Equipment hatEquip = inv.getEquipById("hat");
        float equipMod = 0;
        if (hatEquip != null)
        {
            equipMod = hatEquip.getEffect();
        }

        effectiveAtkCD = baseAtkCD * (1f - equipMod);
        shoot.setAtkCD(effectiveAtkCD);
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
        calcSpeed();
        calcStamina();
        calcAtkCD();
    }

    public float getEffSpd()
    {
        return effectiveSpd;
    }

    private IEnumerator activateIFrame()
    {
        invulOn = true;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(1f);
        renderer.color = new Color(1, 1, 1);
        invulOn = false;
    }

    public void popupMsg(string msg)
    {
        pop.generatePopup(msg);
    }
}
