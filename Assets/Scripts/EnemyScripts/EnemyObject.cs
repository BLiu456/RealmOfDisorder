using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    //If any of the stats can not be loaded, 1 will be the default value
    public int power = 1;
    public int speed = 1;
    public int dropRate = 1;
    public int cost = 1;

    public EnemyData data;

    public GameObject player; 

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        GetComponent<Health>().setHealthValues(data.health, data.health);
        power = data.power;
        speed = data.speed;
        dropRate = data.dropRate;
        cost = data.cost;
    }

    public int getCost() 
    {
        //This function is primarily used by the spawner
        return data.cost;
    }

    public int applyDamage()
    {
        return power;
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
