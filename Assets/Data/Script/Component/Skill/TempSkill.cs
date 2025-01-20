using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TempSkill : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Skill")]
    // Should not be touch directly by Child
    [SerializeField] protected Transform owner;
    [SerializeField] protected int manaCost;
    [SerializeField] protected int hpCost;
    [SerializeField] protected int skillOrder;
    [SerializeField] protected Cooldown skillCD;
    [SerializeField] protected bool isRecharging;

    //==========================================Get Set===========================================
    public Transform Owner => owner;
    public int ManaCost => manaCost;
    public int HpCost => hpCost;
    public int SkillOrder => skillOrder;
    public Cooldown SkillCD => skillCD;
    public bool IsRecharging => isRecharging;

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        if (this.isRecharging) this.Recharging();
    }

    //===========================================Method===========================================
    public void SetSkillOrder(int value)
    {
        this.skillOrder = value;
    }

    public void SetOwner(Transform owner)
    {
        this.owner = owner;
    }
    
    protected void Recharging()
    {
        this.skillCD.CoolingDown();
    }

    protected void ResetSkillCD()
    {
        this.skillCD.ResetStatus();
        this.isRecharging = true;
    }

    protected void DefaultSkillStat(int manaCost, int hpCost, Cooldown skillCD, bool isRecharging)
    {
        this.manaCost = manaCost;
        this.hpCost = hpCost;
        this.skillCD = skillCD;
        this.isRecharging = isRecharging;
    }

    //==========================================Abstract==========================================
    public virtual void MyLoadComponents() { }
    public abstract void MyFixedUpdate();
    public abstract void MyUpdate();
    public abstract void UseSkill();
    public abstract void ResetSkill();
}
