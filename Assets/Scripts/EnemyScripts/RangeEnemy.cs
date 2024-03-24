using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyObject
{
    public void Awake()
    {
        base.Awake(); 
    }

    public void Update()
    {
        movement();
    }

    public void movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public virtual void shoot() { }
}
