using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : Equipment
{
    public float buffAmount = 1f;

    void Awake()
    {
        id = "hat";
        popMsg = "ATK CD DOWN";
        this.effect();
    }

    public override void effect()
    {
        buffAmount = 0.75f * (level / (level + 3));
    }

    public override float getEffect()
    {
        effect();
        return buffAmount;
    }

    public override Equipment clone()
    {
        return (Hat)this.MemberwiseClone();
    }
}
