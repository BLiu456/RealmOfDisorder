using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Dictionary<int, GameObject> enemyDict;
    private Dictionary<int, int> costDict;

    public Camera cam;
    private float height;
    private float width;

    public int bank = 0;

    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize + 1;
        width = cam.orthographicSize * cam.aspect + 1;

        GameObject[] enemies = Resources.LoadAll<GameObject>("Prefabs/Enemy Prefabs");

        enemyDict = new Dictionary<int, GameObject>(enemies.Length);
        costDict = new Dictionary<int, int>(enemies.Length);

        int key = 0;

        foreach (GameObject x in enemies)
        {
            enemyDict[key] = x;
            x.GetComponent<EnemyObject>().Awake(); //Need to wake up the object first so that the cost is set
            costDict[key] = x.GetComponent<EnemyObject>().getCost();
            key++;
        }

        //Sort by cost from most expensive to cheapest
        costDict = costDict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
    }

    public void spawnEnemies()
    {
        int numToSpawn = 0;
        GameObject enemySpawn;
        foreach (KeyValuePair<int, int> x in costDict)
        {
            while (x.Value <= bank)
            {
                numToSpawn += 1;
                bank -= x.Value;

                /*Add a 20% chance to stop spawning this enemy and move to the next one.
                 This should create some variance in the waves of enemies it spawns*/
                int skip = Random.Range(1, 101);
                if (skip <= 20)
                {
                    break;
                }
            }

            enemySpawn = enemyDict[x.Key];
            for (int i = 0; i < numToSpawn; i++)
            {
                Vector3 pos = getRandLoc();
                Instantiate(enemySpawn, pos, Quaternion.identity);
            }
            numToSpawn = 0;
        }
    }

    public void addCredits(int amount)
    {
        bank += amount;
    }

    private Vector3 getRandLoc()
    {
        float wOffset = 0f;
        float hOffset = 0f;

        int dir = Random.Range(0, 4); //Decides which side the enemey randomly spawns at offscreen.
        switch (dir)
        {
            case 0:
                wOffset = width;
                hOffset = Random.Range(-height, height);
                break;
            case 1:
                wOffset = Random.Range(-width, width);
                hOffset = height;
                break;
            case 2:
                wOffset = width * -1f;
                hOffset = Random.Range(-height, height);
                break;
            case 3:
                wOffset = Random.Range(-width, width);
                hOffset = height * -1f;
                break;
        }

        Vector3 pos = new Vector3(cam.transform.position.x + wOffset,
            cam.transform.position.y + hOffset, 
            0);

        return pos;
    }
}
