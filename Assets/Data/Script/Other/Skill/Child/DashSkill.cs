using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : Skill
{
    //==========================================Variable==========================================
    [SerializeField] protected Cooldown dashCD;
    [SerializeField] protected Vector2 dashDir;
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected bool isUsingSkill;
    [SerializeField] protected bool isRechargingSkill;
    
    //========================================Constructor=========================================
    public DashSkill(int manaCost, int hpCost, Cooldown skillCD, float dashSpeed) 
        : base(manaCost, hpCost, skillCD)
    {
        this.dashSpeed = dashSpeed;
        this.isUsingSkill = false;
        this.isRechargingSkill = false;
    }

    //===========================================Method===========================================
    public void UseDash(Vector2 dashDir)
    {
        if (!this.skillCD.IsReady) return;
        this.dashDir = dashDir;
        this.isUsingSkill = true;
        this.isRechargingSkill = false;
        this.skillCD.ResetStatus();
    }

    public void FinishDash()
    {
        if (!this.dashCD.IsReady) return;
        this.isUsingSkill = false;
        this.isRechargingSkill = true;
        this.skillCD.ResetStatus();
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
        rb.velocity = Vector2.zero;
        MovementUtil.Instance.Move(rb, this.dashSpeed, this.dashDir);
    }
}
