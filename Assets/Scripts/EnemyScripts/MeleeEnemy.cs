using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyObject
{
    // Start is called before the first frame update
    public void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    public void Update()
    {
        movement();
    }

    public override void movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.GetComponent<Health>().damaged(power); //For now make it so that the enemies also get hurt when they collide with player

            if (this.GetComponent<Health>().isAlive == false)
            {
                onDeath();
            }
        }
    }

    public override void onDeath()
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
