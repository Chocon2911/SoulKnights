using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireEffect
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
    public FireEffect(float duration,  float waitTime, float damage)
    {
        this.cd = new Cooldown(duration, waitTime);
        this.dealDamageCD = new Cooldown(1, waitTime);
        this.damage = damage;
    }
}