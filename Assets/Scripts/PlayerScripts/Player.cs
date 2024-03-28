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
    private Shooting shoot;

    void Start()
    {
        effectivePower = basePower;
        effectiveHp = baseHp;
        GetComponent<Health>().setHealthValues(effectiveHp, effectiveHp);
        this.GetComponent<HealthUI>().changeBar();

        shoot.setAtkCD(atkCD);
        shoot.updatePower(effectivePower);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.GetComponent<PlayerMovement>().getDashState() == false)
        {
            if (collision.tag == "Enemy")
            {
                EnemyObject enemyComp = collision.GetComponent<EnemyObject>();
                this.GetComponent<Health>().damaged(enemyComp.applyDamage());
                this.GetComponent<HealthUI>().changeBar();
            }
            else if (collision.tag == "Enemy_Atk")
            {
                Projectile projComp = collision.GetComponent<Projectile>();
                this.GetComponent<Health>().damaged(projComp.applyDamage());
                this.GetComponent<HealthUI>().changeBar();
            }

            if (this.GetComponent<Health>().isAlive == false)
            {
                //Handle how player dies here
            }
        }
    }

    public void calcEffHp()
    {
        /*Will do some complicated formula later but for now
         just make it equal to the base stat*/
        effectiveHp = baseHp;
        GetComponent<Health>().setHealthValues(effectiveHp, effectiveHp);
        this.GetComponent<HealthUI>().changeBar();
    }

    public void calcEffPower()
    {
        effectivePower = basePower;
        shoot.updatePower(effectivePower);
    }

    public void scaleStats(int level)
    {
        baseHp += 5 * level;
        basePower += 3 * level;

        calcEffHp();
        calcEffPower();
    }
}
