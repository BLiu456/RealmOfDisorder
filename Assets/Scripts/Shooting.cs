using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    //Data for the bullets
    public string ownerTag = ""; //The entity who is doing the shooting (Player_Atk or Enemey_Atk)
    public string targetTag = ""; //The target the entity is trying to damge (Player or Enemy)
    public int bulletPower = 1;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bullet.tag = ownerTag;
        bullet.GetComponent<Bullet>().setTarget(targetTag);
        bullet.GetComponent<Bullet>().setPower(bulletPower);
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
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }

    public void updatePower(int amount)
    {
        bulletPower = amount;
        bullet.GetComponent<Bullet>().setPower(bulletPower);
    }
}
