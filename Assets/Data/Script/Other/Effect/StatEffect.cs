using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatEffect
{
    //==========================================Variable==========================================
    [SerializeField] private Cooldown cd;
    [SerializeField] private Cooldown dealDamageCD;
    [SerializeField] private int damage;

    //========================================Constructor=========================================
    public StatEffect(float duration, float waitTime, int damage)
    {
        this.cd = new Cooldown(duration, waitTime);
        this.dealDamageCD = new Cooldown(1, waitTime);
        this.damage = damage;
    }

    //===========================================Method===========================================
    public void ActivateEff(float duration, int damage)
    {
        this.cd.ResetStatus();
        this.cd.TimeLimit = duration;
        this.damage = damage;
    }
    
    public void CoolingDown()
    {
        if (this.cd.IsReady) return;
        this.cd.CoolingDown();

        if (!this.cd.IsReady) return;
        this.dealDamageCD.ResetStatus();
    }

    public void DealingDamage(ref int hp)
    {
        if (this.cd.IsReady) return;
        this.dealDamageCD.CoolingDown();

        if (!this.dealDamageCD.IsReady) return;
        this.DealDamage(ref hp);
    }

    private void DealDamage(ref int hp)
    {
        hp -= this.damage;
    }
}