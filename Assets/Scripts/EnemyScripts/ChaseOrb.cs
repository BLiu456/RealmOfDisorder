using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseOrb : Projectile
{
    private float time;
    public float speed = 1;

    public GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
        time += Time.deltaTime;
        if (time >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        //Do nothing, this is so the orb doesnt despawn after colliding with player
    }

    public void setSpeed(float s)
    {
        speed = s;
    }
}
