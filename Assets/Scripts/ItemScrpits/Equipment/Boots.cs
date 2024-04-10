using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Equipment
{
    public float buffAmount = 2f;
    public float buffMod = 2f;

    void Awake()
    {
        id = "boots";
        this.effect();
    }

    public override void effect()
    {
        buffAmount = buffMod * level;
    }

    public override float getEffect()
    {
        effect();
        return buffAmount;
    }

    public override Equipment clone()
    {
        return (Boots)this.MemberwiseClone();
    }
}
