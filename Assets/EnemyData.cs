using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Enemy", order = 1)]

public class EnemyData : ScriptableObject
{
    public int health;
    public int power;
    public int speed;
}
