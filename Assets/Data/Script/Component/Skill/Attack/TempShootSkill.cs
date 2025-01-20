using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TempShootSkill : TempSkill
{
    //==========================================Variable==========================================
    [Header("Shoot")]
    [SerializeField] protected ShootUser user;
    [SerializeField] protected Transform bulletObj;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
    }

    //==========================================Override==========================================
    public override void MyLoadComponents()
    {
        base.MyLoadComponents();
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
    }
    public override void MyFixedUpdate()
    {
        
    }

    public override void MyUpdate()
    {
        if (this.user.CanUseSkill(this)
            && this.user.CanShoot()[this.skillOrder - 1]
            && this.skillCD.IsReady) this.UseSkill();
    }
}
