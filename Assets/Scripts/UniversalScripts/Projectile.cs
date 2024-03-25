using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Liftime")]
    private float timer;
    public float lifetime;

    [Header("Stats")]
    public float force;
    public int power = 1;

    [Header("Positioning")]
    public Vector3 dir;
    public Vector3 rot;

    [Header("Tags")]
    public string targetTag;

    private Rigidbody2D rb;
    
    void Start()
    {
        timer = 0f;

        //Calculate the direction the bullet will move towards
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2 (dir.x, dir.y).normalized * force;
        float angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90); 
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            Destroy(gameObject);
        }
    }

    public int applyDamage()
    {
        return power;
    }

    public void setPower(int amount)
    {
        power = amount;
    }

    public void setForce(float f)
    {
        force = f;
    }

    public void setTarget(string target)
    {
        targetTag = target;
    }

    public void setLifetime(float lt)
    {
        lifetime = lt;
    }

    public void setDir(Vector3 pos)
    {
        dir = pos;
    }

    public void setRot(Vector3 turn)
    {
        rot = turn;
    }
}
