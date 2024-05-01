using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    [Header("UI")]
    public TextMeshProUGUI worldMsg;
    public string[] msgs;

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
        StartCoroutine(display());
    }

    private void increaseSpawnRate()
    {
        /*
         Initially have the spawn rate grow exponentially, it will take about 3 minutes to reach the max credit.
         Afterwards linearly increase it by 550. This is to slow down the amount of enemies spawning at once,
         and won't lag the game quickly. 
         */
        spawnCredit += spawnCoef * Mathf.Pow(2, 1.5f * world_lvl);
        if (spawnCredit > maxCredit) 
        {
            spawnCredit = maxCredit;
            maxCredit += 550;
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

    public void spawnOff()
    {
        canSpawn = false;
    }

    private IEnumerator display()
    {
        if (msgs.Length != 0)
        {
            worldMsg.text = msgs[Random.Range(0, msgs.Length)];
        };
        worldMsg.enabled = true;
        yield return new WaitForSeconds(3);
        worldMsg.enabled = false;
    }
}
