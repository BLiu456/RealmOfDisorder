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
    public int baseExp = 1;
    public int effExp;
    public int dropRate = 1;
    public int cost = 1;

    public EnemyData data;

    public GameObject player;

    public GameObject gm;

    public AudioSource audioSource;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GameManager");

        baseHp = data.health;
        basePwr = data.power;
        speed = data.speed;
        baseExp = data.exp;
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

    public virtual void scaleStats()
    {
        float lvl = gm.GetComponent<GameMaster>().getLvl();
        effHp = (int)((float)baseHp * Mathf.Pow(2.5f, lvl));
        effPwr = (int)((float)basePwr * Mathf.Pow(2f, lvl));
        effExp = (int)((float)baseExp * (1f + lvl));

        GetComponent<Health>().setHealthValues(effHp, effHp);
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player_Atk"))
        {
            Projectile projComp = other.GetComponent<Projectile>();
            this.GetComponent<Health>().damaged(projComp.applyDamage());
            StartCoroutine(damageFlash());
            audioSource.Play();

            if (this.GetComponent<Health>().isAlive == false)
            {
                onDeath();
            }
        }
    }

    public virtual void onDeath()
    {
        player.GetComponent<PlayerLevel>().addExp(effExp);

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

    private IEnumerator damageFlash()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.1f);
        renderer.color = new Color(1, 1, 1);
    }
}
