using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDashSkill : TempSkill
{
    //==========================================Variable==========================================
    [Header("Dash")]
    [SerializeField] private DashUser user;
    [SerializeField] private Cooldown dashCD;
    [SerializeField] private Vector2 dashDir;
    [SerializeField] private float dashSpeed;
    [SerializeField] private bool isDashing;

    //===========================================Method===========================================
    private void Dashing()
    {
        this.user.OnDashing();
        this.user.GetRb().velocity = this.dashDir * this.dashSpeed;
    }

    private void FinishDash()
    {
        this.ResetSkillCD();
        this.isDashing = false;
        this.dashCD.ResetStatus();
    }

    //==========================================Override==========================================
    public override void MyFixedUpdate()
    {
        if (this.isRecharging) this.Recharging();
        if (this.isDashing) this.Dashing();
        if (this.dashCD.IsReady) this.FinishDash();
    }

    public override void MyUpdate()
    {
        
    }

    public override void UseSkill()
    {
        if (!this.user.CanUseSkill(this) || !this.user.CanDash() || !this.skillCD.IsReady) return;
        this.dashDir = this.user.GetDashDir();

        if (dashDir == Vector2.zero) return;
        this.isDashing = true;
        this.user.ConsumePower(this);
    }

    public override void ResetSkill()
    {
        this.ResetSkillCD();
        this.FinishDash();
        this.isRecharging = true;
    }
}
