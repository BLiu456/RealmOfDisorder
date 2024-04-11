using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DireWolf : MeleeEnemy
{
    public float spdScaler = 2f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        scaleSpd();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(slowlyTurn(transform, player.transform, 0.3f));
        rb.velocity = -1 * transform.up * speed;
    }

    IEnumerator slowlyTurn(Transform me, Transform target, float duration)
    {
        Quaternion start = me.rotation;

        float offset = 90f;
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion end = Quaternion.Euler(Vector3.forward * (angle + offset));

        float rotateTime = 0f;

        while (rotateTime < duration)
        {
            me.rotation = Quaternion.Slerp(start, end, rotateTime / duration);
            yield return null;
            rotateTime += Time.deltaTime;
        }
    }

    public void scaleSpd()
    {
        float lvl = gm.GetComponent<GameMaster>().getLvl();
        speed = speed + (int)(spdScaler * lvl);
    }
}
