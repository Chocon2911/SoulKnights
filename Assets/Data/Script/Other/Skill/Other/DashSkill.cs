using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class DashSkill : Skill
{
    //==========================================Variable==========================================
    [SerializeField] protected Cooldown dashCD;
    [SerializeField] protected Vector2 dashDir;
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected bool isUsingSkill;
    [SerializeField] protected bool isRechargingSkill;

    //==========================================Get Set===========================================
    public bool IsUsingSkill { get => isUsingSkill; set => isUsingSkill = value; }
    public bool IsRechargingSkill { get => isRechargingSkill; set => isRechargingSkill = value; }

    //========================================Constructor=========================================
    public DashSkill(int manaCost, int hpCost, Cooldown skillCD, Cooldown dashCD, float dashSpeed) : 
        base(manaCost, hpCost, skillCD)
    {
        this.dashCD = dashCD;
        this.dashSpeed = dashSpeed;
        this.isUsingSkill = false;
        this.isRechargingSkill = false;
    }

    public DashSkill(DashSkillSO so, float waitTime) :
        base(so.ManaCost, so.HpCost, new Cooldown(so.SkillRechargeTime, waitTime))
    {
        this.dashCD = new Cooldown(so.DashTime, waitTime);
        this.dashSpeed = so.DashSpeed;
        this.isUsingSkill = false;
        this.isRechargingSkill = false;
    }


    //===========================================Method===========================================
    public void UseDash(HpReceiver hpRecv, ManaReceiver manaRecv, Vector2 dashDir)
    {
        if (!this.skillCD.IsReady || !this.CanUseSkill(hpRecv.GetCurrHp(), manaRecv.GetCurrMana())) return;
        this.dashDir = dashDir;
        this.isUsingSkill = true;
        this.isRechargingSkill = false;
        SkillUtil.Instance.ConsumeHp(hpRecv, this);
        SkillUtil.Instance.ConsumeMana(manaRecv, this);
        this.skillCD.ResetStatus();
    }

    public void FinishDash()
    {
        if (!this.dashCD.IsReady) return;
        this.isUsingSkill = false;
        this.isRechargingSkill = true;
        this.dashCD.ResetStatus();
    }

    public void DashRecharging()
    {
        if (!this.isRechargingSkill) return;
        this.skillCD.CoolingDown();
    }

    public void Dashing(Rigidbody2D rb)
    {
        if (!this.isUsingSkill) return;
        this.dashCD.CoolingDown();
        MovementUtil.Instance.Move(rb, this.dashSpeed, this.dashDir);
    }
}
