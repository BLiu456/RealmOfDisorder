using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : RangeEnemy
{
    private bool atkState;
    private float timer;
    private bool canFire;
    public float fireTiming;
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
        projComp.setForce(40);
    }

    public void summonOrb()
    {
        GameObject orb_instance = Instantiate(orb, transform.position, Quaternion.identity);

        ChaseOrb orbComp = orb_instance.GetComponent<ChaseOrb>();
        orbComp.setTarget("Player");
        orbComp.setLifetime(orbLife);
        orbComp.setSpeed(player.GetComponent<Player>().getEffSpd());
        orbComp.setPower(effPwr);
    }

    private IEnumerator atk_behavior()
    {
        atkState = true;
        int atkOp = Random.Range(0, 2);
        yield return new WaitForSeconds(1.5f);
        if (atkOp == 0)
        {   
            shoot();
            yield return new WaitForSeconds(0.5f);
            shoot();
            yield return new WaitForSeconds(0.5f);
            shoot();
            yield return new WaitForSeconds(2f);
        }
        else
        {
            summonOrb();
            yield return new WaitForSeconds(orbLife / 3);
        }
        atkState = false;
    }
}
