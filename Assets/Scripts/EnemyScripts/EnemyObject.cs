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
    public int cost = 1;

    public EnemyData data;

    public GameObject player; 

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        GetComponent<Health>().setHealthValues(data.health, data.health);
        power = data.power;
        speed = data.speed;
        dropRate = data.dropRate;
        cost = data.cost;
    }

    public int getCost() 
    {
        //This function is primarily used by the spawner
        return data.cost;
    }

    public virtual void movement(){}

    public virtual void OnTriggerEnter2D(Collider2D other) {}

    public virtual void onDeath(){}
}
