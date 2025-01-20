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

    //===========================================Unity============================================
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (this.isDashing) this.Dashing();
        if (this.dashCD.IsReady) this.FinishDash();
    }

    private void Update()
    {
        if (this.user.CanUseSkill(this) 
            && this.user.CanDash() 
            && this.skillCD.IsReady) this.UseSkill();
    }

    //===========================================Method===========================================
    private void Dashing()
    {
        this.dashCD.CoolingDown();
        this.user.OnDashing();
        this.user.GetRb().velocity = this.dashDir * this.dashSpeed;
    }

    private void FinishDash()
    {
        this.isDashing = false;
        this.isRecharging = true;
        this.dashCD.ResetStatus();
        this.user.OnFinishDashing();
    }

    //==========================================Override==========================================
    public override void MyLoadComponents()
    {
        base.MyLoadComponents();
        this.LoadSO(ref this.so, "SO/Skill/Other/Dash/" + this.owner.name);
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
    }

    public override void MyUpdate()
    {
        if (this.user.CanUseSkill(this)
            && this.user.CanDash()
            && this.skillCD.IsReady) this.UseSkill();
    }

    public override void MyFixedUpdate()
    {
        if (this.isDashing) this.Dashing();
        if (this.dashCD.IsReady) this.FinishDash();
    }

    public override void DefaultStat()
    {
        base.DefaultStat();
        DashSkillSO dashSO = (DashSkillSO)this.so;
        if (dashSO == null)
        {
            Debug.LogError("DashSkillSO is null", transform.gameObject);
            return;
        }

        this.dashSpeed = dashSO.DashSpeed;
        this.dashCD = new Cooldown(dashSO.DashTime, Time.fixedDeltaTime);
        this.skillCD = new Cooldown(dashSO.SkillRechargeTime, Time.fixedDeltaTime);
    }

    public override void UseSkill()
    {
        this.dashDir = this.user.GetDashDir();

        if (dashDir == Vector2.zero) return;
        this.isDashing = true;
        this.isRecharging = false;
        this.user.ConsumePower(this);
        this.ResetSkillCD();
    }

    public override void ResetSkill()
    {
        base.ResetSkill();
        this.FinishDash();
    }
}
