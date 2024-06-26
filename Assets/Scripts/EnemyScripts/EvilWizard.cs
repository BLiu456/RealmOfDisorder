using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : RangeEnemy
{
    private bool atkState;
    private float timer;
    private bool canFire;
    public float fireTiming;
    public int numProj = 3;
    public float orbLife = 5f;

    public GameObject projectile;
    public GameObject orb;
    public Sprite atkSprite;

    private Camera cam;
    private float height;
    private float width;

    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize + 1;
        width = cam.orthographicSize * cam.aspect + 1;

        timer = 0f;
        canFire = true;
        atkState = false;
    }

    void Update()
    {
        if (canFire)
        {
            canFire = false;
            StartCoroutine(atk_behavior());
        }
        else if (!atkState)
        {
            timer += Time.deltaTime;
            if (timer > fireTiming)
            {
                teleport();
                canFire = true;
                timer = 0f;
            }
        }
    }

    public void teleport()
    {
        float xCord = Random.Range(-width, width);
        float yCord = Random.Range(-height, height);

        Vector3 pos = new Vector3(cam.transform.position.x + xCord,
            cam.transform.position.y + yCord, 0);

        transform.position = pos;
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
        projComp.setLifetime(3f);
        projComp.setPower(this.effPwr / 2);
        projComp.setSprite(atkSprite);
        projComp.setForce(50);
    }

    public void summonOrb()
    {
        GameObject orb_instance = Instantiate(orb, transform.position, Quaternion.identity);

        ChaseOrb orbComp = orb_instance.GetComponent<ChaseOrb>();
        orbComp.setTarget("Player");
        orbComp.setLifetime(orbLife);
        orbComp.setSpeed(player.GetComponent<Player>().getEffSpd());
        orbComp.setPower((3 * effPwr) / 4); //Orb has 75% of evil wizards atk
    }

    public override void scaleStats()
    {
        float lvl = gm.GetComponent<GameMaster>().getLvl();
        effHp = (int)((float)baseHp * Mathf.Pow(4f, lvl) + (100f * Mathf.Pow(lvl, 2)));
        effPwr = (int)((float)basePwr * Mathf.Pow(3f, lvl) + (10f * Mathf.Pow(lvl, 2)));
        effExp = (int)((float)baseExp * Mathf.Pow(1.5f, lvl));
        numProj += (int)lvl - 2;

        GetComponent<Health>().setHealthValues(effHp, effHp);
    }
    
    private IEnumerator atk_behavior()
    {
        atkState = true;
        int atkOp = Random.Range(0, 2);
        yield return new WaitForSeconds(1.5f);
        if (atkOp == 0)
        {   
            for (int i = 0; i < numProj; i++)
            {
                shoot();
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(1.5f);
        }
        else
        {
            summonOrb();
            yield return new WaitForSeconds(orbLife / 3);
        }
        atkState = false;
    }
}
