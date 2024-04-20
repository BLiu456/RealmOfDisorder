using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : RangeEnemy
{
    Animator myAnimator;
    private float timer;
    private bool canFire;
    private bool canMove;
    public float fireTiming;

    public GameObject projectile;

    public Sprite atkSprite;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        timer = 0f;
        canFire = true;
        canMove = true;
    }

    void Update()
    {
        if (canFire)
        {
            canFire = false;
            StartCoroutine(atk_behavior());
        }
        else if (canMove)
        {
            base.movement();
            timer += Time.deltaTime;
            if (timer > fireTiming)
            {
                canFire = true;
                timer = 0;
            }
        }
    }

    public override void shoot()
    {
        //Direct attack towards player
        projectile.GetComponent<Projectile>().setDir(player.transform.position - transform.position);
        projectile.GetComponent<Projectile>().setRot(transform.position - player.transform.position);
        GameObject proj_instance = Instantiate(projectile, transform.position, Quaternion.identity);

        //Set properties of the attack
        Projectile projComp = proj_instance.GetComponent<Projectile>();
        proj_instance.tag = "Enemy_Atk";
        projComp.setTarget("Player");
        projComp.setLifetime(10f);
        projComp.setPower(this.effPwr);
        projComp.setSprite(atkSprite);
        projComp.setForce(5);
    }

    private IEnumerator atk_behavior()
    {
        canMove = false;
        myAnimator.SetTrigger("Shoot");
        yield return new WaitForSeconds(1);
        shoot();
        yield return new WaitForSeconds(1);
        canMove = true;
    }
}
