using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //If any of the stats can not be loaded, 1 will be the default value
    [SerializeField]
    public int health = 1;

    [SerializeField]
    public int power = 1;

    [SerializeField]
    public int speed = 1;

    public EnemyData data;

    private GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public virtual void movement()
    {
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public virtual void OnCollision(Collider2D other)
    {

    }
}
