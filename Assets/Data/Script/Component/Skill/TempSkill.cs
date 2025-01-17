using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TempSkill : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Skill")]
    // Should not be touch directly by Child
    [SerializeField] protected int manaCost;
    [SerializeField] protected int hpCost;
    [SerializeField] protected Cooldown skillCD;
    [SerializeField] protected bool isRecharging;

    //==========================================Get Set===========================================
    public int ManaCost => manaCost;
    public int HpCost => hpCost;
    public Cooldown SkillCD => skillCD;
    public bool IsRecharging => isRecharging;

    //===========================================Unity============================================
    protected virtual void FixedUpdate()
    {
        if (this.isRecharging) this.Recharging();
    }

    //===========================================Method===========================================
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
    public abstract void UseSkill();
    public abstract void ResetSkill();
}
