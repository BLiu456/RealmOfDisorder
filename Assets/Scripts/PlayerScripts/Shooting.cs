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

    //Data for the bullets
    public string ownerTag = ""; //The entity who is doing the shooting (Player_Atk or Enemey_Atk)
    public string targetTag = ""; //The target the entity is trying to damge (Player or Enemy)
    public int rangePower = 1;
    public Sprite projSprite;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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

            //Aim attack towards mouse
            proj.GetComponent<Projectile>().setDir(mousePos - bulletTransform.position);
            proj.GetComponent<Projectile>().setRot(bulletTransform.position - mousePos);
            GameObject proj_instance = Instantiate(proj, bulletTransform.position, Quaternion.identity);

            //Set properties of the attack
            Projectile projComp = proj_instance.GetComponent<Projectile>();
            proj_instance.tag = ownerTag;
            projComp.setTarget(targetTag);
            projComp.setLifetime(bulletLife);
            projComp.setPower(rangePower);
            projComp.setSprite(projSprite);
            projComp.setForce(bulletSpd);
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
}
