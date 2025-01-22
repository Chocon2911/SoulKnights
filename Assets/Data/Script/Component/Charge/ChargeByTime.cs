using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeByTime : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Charge By Time")]
    [SerializeField] private ChargeByTimeUser user;
    [SerializeField] private Transform owner;
    [SerializeField] private Cooldown chargeCD;
    [SerializeField] private bool canCharge;

    //==========================================Get Set===========================================
    public ChargeByTimeUser User { get => user; set => user = value; }
    public Transform Owner { get => owner; set => owner = value; }
    public Cooldown ChargeCD { get => chargeCD; set => chargeCD = value; }
    public bool CanCharge { get => canCharge; set => canCharge = value; }

    //===========================================Method===========================================
    public void FinishCharge()
    {
        this.canCharge = false;
        this.chargeCD.ResetStatus();
        this.user.OnFinishCharging();
    }

    private void Charge()
    {
        if (this.chargeCD.IsReady) return;
        this.chargeCD.CoolingDown();
        this.user.OnCharging();
    }

    //===========================================Other============================================
    public virtual void MyLoadComponent()
    {
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
    }

    public virtual void MyUpdate()
    {
        // For Override
    }

    public virtual void MyFixedUpdate()
    {
        if (this.canCharge) this.Charge();
    }
    
    public virtual void ResetCharge()
    {
        this.chargeCD.ResetStatus();
        this.canCharge = true;
    }
}
