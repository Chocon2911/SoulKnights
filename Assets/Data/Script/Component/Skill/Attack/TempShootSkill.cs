using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TempShootSkill : TempSkill
{
    //==========================================Variable==========================================
    [Header("Shoot")]
    [SerializeField] protected ShootUser user;
    [SerializeField] protected Transform bulletObj;

    //===========================================Unity============================================
    protected virtual void Update()
    {
        if (this.user.CanUseSkill(this)
            && this.user.CanShoot()
            && this.skillCD.IsReady) this.UseSkill();
    }
}
