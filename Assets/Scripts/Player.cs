using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int maxHp = 3;
    public int basePower = 5;
    public int effectivePower = 1;
    //public float bulletCoolDown; Maybe repurpose this to attack speed
    //float bulletTimer;

    void Start()
    {
        effectivePower = basePower;
        GetComponent<Health>().setHealthValues(maxHp, maxHp);
        setProjPower();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyObject enemyComp = collision.GetComponent<EnemyObject>();
            this.GetComponent<Health>().damaged(enemyComp.applyDamage());
        }
        else if (collision.tag == "Enemy_Atk")
        {
            Projectile projComp = collision.GetComponent<Projectile>();
            this.GetComponent<Health>().damaged(projComp.applyDamage());
        }

        if (this.GetComponent<Health>().isAlive == false)
        {
            //Handle how player dies here
        }
    }

    public void setProjPower()
    {
        Component[] x = GetComponentsInChildren<Shooting>();

        foreach (Shooting i in x)
        {
            i.updatePower(effectivePower);
        }
    }
}
