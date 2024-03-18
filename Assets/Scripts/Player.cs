using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHp = 3;
    public int basePower = 5;
    public int effectivePower = 1;
    //public float bulletCoolDown; Maybe repurpose this to attack speed

    float bulletTimer;

    void Start()
    {
        effectivePower = basePower;
        GetComponent<Health>().setHealthValues(maxHp, maxHp);
        setPower();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyObject enemyComp = collision.GetComponent<EnemyObject>();
            this.GetComponent<Health>().damaged(enemyComp.applyDamage());
        }

        if (this.GetComponent<Health>().isAlive == false)
        {
            //Handle how player dies here
        }
    }

    public void setPower()
    {
        Component[] x = GetComponentsInChildren<Shooting>();

        foreach (Shooting i in x)
        {
            i.updatePower(effectivePower);
        }
    }
}
