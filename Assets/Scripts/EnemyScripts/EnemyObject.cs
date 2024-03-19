using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    //If any of the stats can not be loaded, 1 will be the default value
    public int baseHp = 1;
    public int effHp;
    public int basePwr = 1;
    public int effPwr;
    public int speed = 1;
    public int dropRate = 1;
    public int cost = 1;

    public EnemyData data;

    public GameObject player;

    public GameObject gm;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GameManager");

        baseHp = data.health;
        basePwr = data.power;
        speed = data.speed;
        dropRate = data.dropRate;
        cost = data.cost;

        scaleStats();
    }

    public int getCost() 
    {
        return cost;
    }

    public int applyDamage()
    {
        return effPwr;
    }

    public void scaleStats()
    {
        float lvl = gm.GetComponent<GameMaster>().getLvl();
        effHp = (int)((float)baseHp * (1f + (0.5f * lvl)));
        effPwr = (int)((float)basePwr * (1f + (0.5f * lvl)));

        GetComponent<Health>().setHealthValues(effHp, effHp);
    }

    public virtual void movement(){}

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player_Atk"))
        {
            Bullet bullComp = other.GetComponent<Bullet>();
            this.GetComponent<Health>().damaged(bullComp.applyDamage());

            if (this.GetComponent<Health>().isAlive == false)
            {
                onDeath();
            }
        }
    }

    public virtual void onDeath()
    {
        int dropped = Random.Range(1, 101); //Get range from 1 - 100

        if (dropped <= dropRate)
        {
            GameObject dropItem = this.GetComponent<LootTable>().selectLoot();

            if (dropItem != null)
            {
                GameObject lootObject = Instantiate(dropItem, transform.position, Quaternion.identity); //Drops an item where enemy died
            }
        }

        Destroy(gameObject);
    }
}
