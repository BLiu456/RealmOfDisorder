using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Enemy", order = 1)]

public class EnemyData : ScriptableObject
{
    public int health;
    public int power;
    public int speed;
    public int dropRate; //dropRate of 1 refers to 1% chance of dropping an item as an example
    public int cost; //How much it costs to pawn this enemy
}
