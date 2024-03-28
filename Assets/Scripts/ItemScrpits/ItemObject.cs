using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObject : MonoBehaviour
{
    public string name;
    public string desc;
    public Sprite sprite;
    
    public virtual void OnTriggerEnter2D(Collider2D other) { }

    public virtual void effect() { }
}
