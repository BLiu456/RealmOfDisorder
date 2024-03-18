using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    private float timer;
    public string targetTag; //Target of the bullet (i.e. the player wants to target enemies)
    public float force;
    public float lifetime;
    public int power = 1;
    
    void Start()
    {
        timer = 0f;

        //Calculate the direction the bullet will move towards
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos; // delete if don't want bullet rotation towards mouse
        rb.velocity = new Vector2 (direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2 (rotation.y, rotation.x) * Mathf.Rad2Deg; // delete if don't want bullet rotation towards mouse
        transform.rotation = Quaternion.Euler(0, 0, rot + 90); // delete if don't want bullet rotation towards mouse
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

    public void setTarget(string target)
    {
        targetTag = target;
    }
}
