using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Equipment
{
    public float buffAmount = 0.2f;
    public float buffModifier = 0.2f;

    void Awake()
    {
        id = "sword";
        popMsg = "ATK UP";
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
