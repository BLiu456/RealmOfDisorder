using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : RangeEnemy
{
    private float timer;
    private bool canFire;
    private bool canMove;
    public float fireTiming;

    public GameObject projectile;

    void Start()
    {
        timer = 0f;
        canFire = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            canFire = false;
            StartCoroutine(atk_behavior());
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

        if (canMove)
        {
            base.movement();
        }
    }

    public override void shoot()
    {
        //Direct attack towards player
        projectile.GetComponent<Projectile>().setDir(player.transform.position - transform.position);
        projectile.GetComponent<Projectile>().setRot(transform.position - player.transform.position);
        GameObject proj_instance = Instantiate(projectile, transform.position, Quaternion.identity);

        //Set properties of the attack
        proj_instance.tag = "Enemy_Atk";
        proj_instance.GetComponent<Projectile>().setTarget("Player");
        proj_instance.GetComponent<Projectile>().setPower(this.effPwr);
    }

    private IEnumerator atk_behavior()
    {
        canMove = false;
        yield return new WaitForSeconds(1);
        shoot();
        yield return new WaitForSeconds(1);
        canMove = true;
    }
}
