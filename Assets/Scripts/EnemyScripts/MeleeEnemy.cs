using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyObject
{
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
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
            this.GetComponent<Health>().damaged(power);

            if (this.GetComponent<Health>().isAlive == false)
            {
                onDeath();
            }
        }
    }

    public override void onDeath()
    {
        Debug.Log("Something is dead");
    }
}
