using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHp = 3;
    public int basePower = 1;
    public int effectivePower = 1;
    public float bulletCoolDown;

    float bulletTimer;

    void Start()
    {
        GetComponent<Health>().setHealthValues(maxHp, maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        bulletTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") //&& bulletTimer <= 0)
        {
            Debug.Log("Player took damage");
            this.GetComponent<Health>().damaged(1);
            bulletTimer = bulletCoolDown;
        }
    }
}
