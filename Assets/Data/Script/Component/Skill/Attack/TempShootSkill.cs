using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TempShootSkill : TempSkill
{
    //==========================================Variable==========================================
    [Header("Shoot")]
    [SerializeField] protected new ShootUser user;
    [SerializeField] protected Transform bulletObj;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
    }

    //==========================================Override==========================================
    public override void MyLoadComponent()
    {
        base.MyLoadComponent();
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
    }
    public override void MyFixedUpdate()
    {
        
    }

    public override void MyUpdate()
    {
        if (this.user.CanUseSkill(this)
            && this.user.CanShoot()[this.user.GetSkillOrder(this)]
            && this.skillCD.IsReady) this.UseSkill();
    }

    public override void DefaultStat()
    {
        base.DefaultStat();
        ShootSkillSO shootSO = (ShootSkillSO)this.so;
        if (shootSO == null)
        {
            Debug.LogError("AttackSkillSO is null", transform.gameObject);
            return;
        }

        this.bulletObj = shootSO.Bullet;
        this.skillCD = new Cooldown(1 / shootSO.FireRate, Time.fixedDeltaTime);
    }
}
