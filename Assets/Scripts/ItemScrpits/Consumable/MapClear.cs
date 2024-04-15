using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClear : ItemObject
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject[] gameObj = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject x in gameObj)
            {
                EnemyObject killSwitch = x.GetComponent<EnemyObject>();
                killSwitch.onDeath();
            }

            GameObject[] pObj = GameObject.FindGameObjectsWithTag("Enemy_Atk");
            foreach (GameObject x in pObj)
            {
                Destroy(x);
            }

            Destroy(gameObject);
        }
    }
}
