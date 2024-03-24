using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : RangeEnemy
{
    private float timer;
    private bool canFire;
    public float fireTiming;

    public GameObject projectile;

    void Start()
    {
        timer = 0f;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
        base.movement();
    }

    public override void shoot()
    {
        if (canFire)
        {
            canFire = false;

            //Direct attack towards player
            projectile.GetComponent<Projectile>().setDir(player.transform.position - transform.position);
            projectile.GetComponent<Projectile>().setRot(transform.position - player.transform.position);
            GameObject proj_instance = Instantiate(projectile, transform.position, Quaternion.identity);

            //Set properties of the attack
            proj_instance.tag = "Enemy_Atk";
            proj_instance.GetComponent<Projectile>().setTarget("Player");
            proj_instance.GetComponent<Projectile>().setPower(this.effPwr);
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > fireTiming)
            {
                canFire = true;
                timer = 0;
            }
        }
    }
}
