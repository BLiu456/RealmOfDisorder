using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipment
{
    public float buffAmount = 0.1f;
    public float buffMod = 0.1f;
    void Awake()
    {
        id = "armor";
        popMsg = "HLTH UP";
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
        return (Armor)this.MemberwiseClone();
    }
}
