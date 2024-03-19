using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
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
        InvokeRepeating("increaseDifficulty", 60.0f, 60.0f); //Every 60 sec increase difficulty
        InvokeRepeating("increaseSpawnRate", 10.0f, 10.0f); //Every 10 sec increase spawn rate
        InvokeRepeating("activateSpawn", 5.0f, 5.0f); //Every 5 sec spawn a wave of enemies
    }

    private void increaseDifficulty()
    {
        world_lvl += 1f;
        spawnCredit += 30;
    }

    private void increaseSpawnRate()
    {
        spawnCredit += 6;
    }

    private void activateSpawn()
    {
        spawn.addCredits(spawnCredit);
        spawn.spawnEnemies();
    }

    public float getLvl()
    {
        return world_lvl;
    }
}
