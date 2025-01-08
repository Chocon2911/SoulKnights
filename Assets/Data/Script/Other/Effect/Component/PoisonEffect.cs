using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoisonEffect
{
    //==========================================Variable==========================================
    [SerializeField] private Cooldown cd; // Poision Duration
    [SerializeField] private Cooldown dealDamageCD; // Deal Damage after a Time
    [SerializeField] private float damage;

    //==========================================Get Set===========================================
    public Cooldown Cd
    {
        get => cd;
        set => cd = value;
    }

    public Cooldown DealDamageCD
    {
        get => dealDamageCD;
        set => dealDamageCD = value;
    }

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    //========================================Constructor=========================================
    public PoisonEffect(float poisonDuration, float waitTime, float damage)
    {
        this.cd = new Cooldown(poisonDuration, waitTime);
        this.dealDamageCD = new Cooldown(1, waitTime);
        this.damage = damage;
    }
}
