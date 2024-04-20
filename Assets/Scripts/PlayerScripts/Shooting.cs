using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject proj;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    public float bulletLife = 8f;
    public float bulletSpd = 15f;
    public int bulletPierce = 1;
    public int numProj = 1;
    public float spread = 0f;
    private AudioSource audioSource;

    //Data for the bullets
    public string ownerTag = ""; //The entity who is doing the shooting (Player_Atk or Enemey_Atk)
    public string targetTag = ""; //The target the entity is trying to damge (Player or Enemy)
    public int rangePower = 1;
    public Sprite projSprite;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,rotZ);

        if(!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring) 
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            shoot();
            audioSource.Play();
        }
    }

    private void shoot()
    {
        //Aim attack towards mouse
        /*proj.GetComponent<Projectile>().setDir(mousePos - bulletTransform.position);
        proj.GetComponent<Projectile>().setRot(bulletTransform.position - mousePos);
        GameObject proj_instance = Instantiate(proj, bulletTransform.position, Quaternion.identity);*/

        //Set properties of the attack
        /*Projectile projComp = proj_instance.GetComponent<Projectile>();
        proj_instance.tag = ownerTag;
        projComp.setTarget(targetTag);
        projComp.setLifetime(bulletLife);
        projComp.setPower(rangePower);
        projComp.setSprite(projSprite);
        projComp.setForce(bulletSpd);*/
        Vector3 mPos = mousePos;
        Vector3 currPos = bulletTransform.position;
        Vector3 dir = (mPos - currPos);
        float radius = -1f * Vector3.Distance(mPos, currPos);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angleStep = spread / (float)(numProj / 2);
        float angleOffset = -1f * spread;

        for (int i = 0; i < numProj; i++)
        {
            float xDir = currPos.x + (Mathf.Cos(((angle + angleOffset) * Mathf.PI) / 180) * radius);
            float yDir = currPos.y + (Mathf.Sin(((angle + angleOffset) * Mathf.PI) / 180) * radius);

            Vector3 projVector = new Vector3(xDir, yDir, 0);
            //Vector3 projDir = mousePos - projVector; 

            proj.GetComponent<Projectile>().setDir(mPos - projVector);
            proj.GetComponent<Projectile>().setRot(projVector - mPos);
            GameObject proj_instance = Instantiate(proj, transform.position, Quaternion.identity);

            //Set properties of the attack
            Projectile projComp = proj_instance.GetComponent<Projectile>();
            proj_instance.tag = ownerTag;
            projComp.setTarget(targetTag);
            projComp.setLifetime(bulletLife);
            projComp.setPower(rangePower);
            projComp.setSprite(projSprite);
            projComp.setForce(bulletSpd);
            projComp.setPierce(bulletPierce);

            angleOffset += angleStep;
        }
    }

    public void updatePower(int amount)
    {
        rangePower = amount;
        proj.GetComponent<Projectile>().setPower(rangePower);
    }

    public void setAtkCD(float x)
    {
        timeBetweenFiring = x;
    }

    public void setBulletLife(float x)
    {
        bulletLife = x;
    }

    public void setBulletSpd(float x)
    {
        bulletSpd = x;
    }

    public void setBulletPierce(int x)
    {
        bulletPierce = x;
    }

    public void setNumProj(int x)
    {
        numProj = x;
    }

    public void setSpread(float a)
    {
        spread = a;
    }
}
