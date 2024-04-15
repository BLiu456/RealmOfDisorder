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
    private float bossTime = 30.0f;
    

    [Header("Difficulty Modifers")]
    public float world_lvl = 0f;
    public float spawnCredit = 10;
    public float maxCredit = 350;
    public Spawner spawn;
    [SerializeField]
    private float spawnCoef;
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
        InvokeRepeating("spawnBoss", bossTime, bossTime);
    }

    private void increaseDifficulty()
    {
        world_lvl += 1f; 
    }

    private void increaseSpawnRate()
    {
        if (spawnCredit <= maxCredit)
        {
            spawnCredit += spawnCoef * Mathf.Pow(2, 1.5f * world_lvl);
        }   
    }

    private void activateSpawn()
    {
        if (canSpawn)
        {
            spawn.addCredits((int)spawnCredit);
            spawn.spawnEnemies();
        }
    }

    private void spawnBoss()
    {
        if (canSpawn)
        {
            spawn.spawnBoss();
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
