using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Dev Settings")]
    [SerializeField]
    private bool canSpawn = true;
    [SerializeField]
    private float worldTimer = 60.0f;
    [SerializeField]
    private float rateTimer = 15.0f;
    [SerializeField]
    private float spawnTime = 6.0f;
    [SerializeField]
    private int spawnCoef = 6;

    [Header("Difficulty Modifers")]
    public float world_lvl = 0f;
    public int spawnCredit = 10;
    public Spawner spawn;

    void Awake()
    {
        spawn = this.GetComponent<Spawner>();
    }

    void Start()
    {
        activateSpawn(); //Spawn initial set of enemies
        InvokeRepeating("increaseDifficulty", worldTimer, worldTimer);
        InvokeRepeating("increaseSpawnRate", rateTimer, rateTimer); 
        InvokeRepeating("activateSpawn", spawnTime, spawnTime); 
    }

    private void increaseDifficulty()
    {
        world_lvl += 1f;
        spawnCredit += spawnCoef * (1 + (int)world_lvl);
    }

    private void increaseSpawnRate()
    {
        spawnCredit += spawnCoef;
    }

    private void activateSpawn()
    {
        if (canSpawn)
        {
            spawn.addCredits(spawnCredit);
            spawn.spawnEnemies();
        }
    }

    public float getLvl()
    {
        return world_lvl;
    }

    public void spawnSwitch()
    {
        canSpawn = !canSpawn;
    }
}
