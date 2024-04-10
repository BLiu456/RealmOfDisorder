using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necklace : Equipment
{
    public float buffAmount = 10f;
    public float buffMod = 10f;

    void Awake()
    {
        id = "necklace";
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
        return (Necklace)this.MemberwiseClone();
    }
}
