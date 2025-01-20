using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TempSkill : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Skill")]
    [SerializeField] protected SkillSO so; 
    [SerializeField] protected Transform owner;
    [SerializeField] protected int manaCost;
    [SerializeField] protected int hpCost;
    [SerializeField] protected Cooldown skillCD;
    [SerializeField] protected bool isRecharging;

    //==========================================Get Set===========================================
    public Transform Owner => owner;
    public int ManaCost => manaCost;
    public int HpCost => hpCost;
    public Cooldown SkillCD => skillCD;
    public bool IsRecharging => isRecharging;

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        if (this.isRecharging) this.Recharging();
    }

    //===========================================Skill============================================
    public void SetOwner(Transform owner)
    {
        this.owner = owner;
    }

    public void SetIsRecharging(bool value)
    {
        this.isRecharging = value;
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

    //===========================================Other============================================
    public virtual void DefaultStat()
    {
        if (this.so == null)
        {
            Debug.LogError("SkillSO is null", transform.gameObject);
            return;
        }
        
        this.manaCost = this.so.ManaCost;
        this.hpCost = this.so.HpCost;
        this.skillCD = new Cooldown(this.so.SkillRechargeTime, Time.fixedDeltaTime);
    }

    public virtual void ResetSkill()
    {
        this.isRecharging = true;
        this.skillCD.ResetStatus();
    }

    //==========================================Abstract==========================================
    public virtual void MyLoadComponents() { }
    public abstract void MyFixedUpdate();
    public abstract void MyUpdate();
    public abstract void UseSkill();
}
