using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClear : ItemObject
{
    public void Start()
    {
        popMsg = "SMITE";
    }

    public override void effect(GameObject obj)
    {
        //Destroy all enemies
        GameObject[] gameObj = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject x in gameObj)
        {
            EnemyObject killSwitch = x.GetComponent<EnemyObject>();
            killSwitch.onDeath();
        }

      /*//Destroy all projectiles   
        GameObject[] pObj = GameObject.FindGameObjectsWithTag("Enemy_Atk");
        foreach (GameObject x in pObj)
        {
            Destroy(x);
        }*/
    }
}
