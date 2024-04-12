using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : RangeEnemy
{
    private float timer;
    private bool canFire;
    public float fireTiming;
    public int minProj = 4;
    public int maxProj = 12;
    private int numProj;
    public float radius = 1f;

    public GameObject projectile;
    public Sprite atkSprite;

    void Start()
    {
        timer = 0f;
        canFire = true;
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
    }

    public override void shoot()
    {
        float angle = 0f;
        float angleOffset = 360f / numProj;

        for (int i = 0; i < numProj; i++)
        {
            float xDir = transform.position.x + (Mathf.Cos(((angle) * Mathf.PI) / 180) * radius);
            float yDir = transform.position.y + (Mathf.Sin(((angle) * Mathf.PI) / 180) * radius);

            Vector3 projVector = new Vector3(xDir, yDir, 0);
            Vector3 projDir = (projVector - transform.position).normalized;

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
            projComp.setForce(10);

            angle += angleOffset;
        }
    }

    private IEnumerator atk_behavior()
    {
        numProj = Random.Range(minProj, maxProj+1); //Shoots a random amount of projectiles from 4 to 16
        shoot();
        yield return new WaitForSeconds(2);
        numProj = Random.Range(minProj, maxProj + 1); //Shoots a random amount of projectiles from 4 to 16
        shoot();
    }
}
