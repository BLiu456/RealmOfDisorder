using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadArcher : RangeEnemy
{
    private float timer;
    private bool canFire;
    private bool canMove;
    public float fireTiming;

    public GameObject projectile;
    public Sprite atkSprite;
    
    void Start()
    {
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
        Vector3 playerPos = player.transform.position;
        Vector3 currPos = transform.position;
        Vector3 dir = (playerPos - currPos);
        float radius = -1f * Vector3.Distance(playerPos, currPos);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angleOffset = -30f;

        for (int i = 0; i < 3; i++)
        {
            float xDir = currPos.x + (Mathf.Cos(((angle + angleOffset) * Mathf.PI) / 180) * radius);
            float yDir = currPos.y + (Mathf.Sin(((angle + angleOffset) * Mathf.PI) / 180) * radius);

            Vector3 projVector = new Vector3(xDir, yDir, 0);
            Vector3 projDir = playerPos - projVector; //Centers attack on player

            projectile.GetComponent<Projectile>().setDir(projDir);
            projectile.GetComponent<Projectile>().setRot(projDir);
            GameObject proj_instance = Instantiate(projectile, transform.position, Quaternion.identity);

            //Set properties of the attack
            Projectile projComp = proj_instance.GetComponent<Projectile>();
            proj_instance.tag = "Enemy_Atk";
            projComp.setTarget("Player");
            projComp.setLifetime(fireTiming);
            projComp.setPower(this.effPwr);
            projComp.setSprite(atkSprite);
            projComp.setForce(12);

            angleOffset += 30f;
        }     
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
