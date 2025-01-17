using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkill : Skill
{
    //==========================================Variable==========================================
    [Header("Shoot Skill")]
    [SerializeField] protected bool isRecharging;

    //==========================================Get Set===========================================
    public bool IsRecharging { get => isRecharging; set => isRecharging = value; }

    //========================================Constructor=========================================
    public AttackSkill(int manaCost, int hpCost, Cooldown skillCD) : 
        base(manaCost, hpCost, skillCD) 
    {
        this.isRecharging = false;
    }

    public AttackSkill(AttackSkillSO so, float waitTime) : 
        base(so.ManaCost, so.HpCost, new Cooldown(so.AttackRate, waitTime))
    {
        this.isRecharging = false;
    }

    //===========================================Method===========================================
    public virtual void Recharging() 
    {
        if (!this.isRecharging) return;
        this.skillCD.CoolingDown();
    }
}
