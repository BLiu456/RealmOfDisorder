using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bear : MeleeEnemy
{
    public float atkTiming;
    public float atkDuration = 1f;
    public float atkSpd = 8f;
    public GameObject spawnObj;
    public Rigidbody2D rb;

    private float timer;
    private bool canAtk;
    private bool canMove;

    private Camera cam;
    private float height;
    private float width;

    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize + 1;
        width = cam.orthographicSize * cam.aspect + 1;

        canAtk = false;
        canMove = true;
        timer = 0f;
    }

    void Update()
    {
        if (canAtk)
        {
            canAtk = false;
            StartCoroutine(atk_behavior());
        }
        else if (canMove)
        {
            timer += Time.deltaTime;
            if (timer > atkTiming)
            {
                canAtk = true;
                timer = 0;
            }

            Vector3 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
        }
    }

    private IEnumerator atk_behavior()
    {
        canMove = false;
        roar();
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1.5f);
        Vector3 dir = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(dir.x * speed * atkSpd, dir.y * speed * atkSpd);
        yield return new WaitForSeconds(atkDuration);
        canMove = true;
    }

    private void roar()
    {
        int numToSpawn = Random.Range(1, 3);

        for (int i = 0; i < numToSpawn; i++)
        {
            float yCord = Random.Range(-height, height);

            Vector3 pos = new Vector3(width,
                cam.transform.position.y + yCord, 0);

            Instantiate(spawnObj, pos, Quaternion.identity);
        }
    }
    public override void scaleStats()
    {
        float lvl = gm.GetComponent<GameMaster>().getLvl();
        effHp = (int)((float)baseHp * Mathf.Pow(4f, lvl));
        effPwr = (int)((float)basePwr * Mathf.Pow(3f, lvl));
        effExp = (int)((float)baseExp * Mathf.Pow(1.5f, lvl));

        GetComponent<Health>().setHealthValues(effHp, effHp);
    }
}
