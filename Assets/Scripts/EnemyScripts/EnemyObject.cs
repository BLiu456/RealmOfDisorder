using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public abstract class EnemyObject : MonoBehaviour
{
    //If any of the stats can not be loaded, 1 will be the default value
    public int power = 1;
    public int speed = 1;
    public int dropRate = 1;

    public EnemyData data;

    public GameObject player; 

    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        GetComponent<Health>().setHealthValues(data.health, data.health);
        power = data.power;
        speed = data.speed;
        dropRate = data.dropRate;
    }

    public virtual void movement(){}

    public virtual void OnTriggerEnter2D(Collider2D other) {}

    public virtual void onDeath(){}
}
