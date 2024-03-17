using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    private float timer;
    public float force;
    public float lifetime;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos; // delete if don't want bullet rotation towards mouse
        rb.velocity = new Vector2 (direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2 (rotation.y, rotation.x) * Mathf.Rad2Deg; // delete if don't want bullet rotation towards mouse
        transform.rotation = Quaternion.Euler(0, 0, rot + 90); // delete if don't want bullet rotation towards mouse
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
