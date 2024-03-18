using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyObject
{
    public void Awake()
    {
        base.Awake();
    }

    public void Update()
    {
        movement();
    }

    public override void movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
