using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Equipment
{
    public float buffAmount = 0.1f;
    public float buffModifier = 0.1f;

    void Awake()
    {
        id = "sword";
        this.effect();
    }

    public override void effect()
    {
        buffAmount = buffModifier * level;
    }

    public override float getEffect()
    {
        effect();
        return buffAmount;
    }

    public override Equipment clone()
    {
        return (Sword)this.MemberwiseClone();
    }
}
