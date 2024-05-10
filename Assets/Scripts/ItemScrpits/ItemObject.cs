using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObject : MonoBehaviour
{
    public string name;
    public string desc;
    public string popMsg;
    public Sprite sprite;
    private float timer = 0;

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 30f) //Despawn object after 30 seconds
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            effect(other.gameObject);
            other.GetComponent<Player>().popupMsg(popMsg);
            Destroy(gameObject);
        }
    }

    public virtual void effect() { }
    public virtual void effect(GameObject obj) { }
}
