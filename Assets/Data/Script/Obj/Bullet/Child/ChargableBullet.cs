using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargableBullet : Bullet
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Chargable Bullet===")]
    [Header("Charge Stat")]
    [SerializeField] private ChargeStat chargeStat;

    [Header("Increase Size")]
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;

    [Header("Increase Speed")]
    [SerializeField] private List<float> moveSpeeds;

    [Header("Increase Damage")]
    [SerializeField] private List<int> damages;

    //===========================================Unity============================================
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.ChargeFUpdate();
    }

    //===========================================Method===========================================
    public virtual void FinishCharge()
    {
        this.chargeStat.FinishCharge();
        this.damage = this.damages[this.chargeStat.TempGrade];
        this.moveSpeed = this.moveSpeeds[this.chargeStat.TempGrade];
        this.bodyCollider.enabled = true;
        this.canMove = true;
        this.canDespawnByTime = true;
    }

    protected virtual void ChargeFUpdate()
    {
        if (!this.chargeStat.IsCharging) return;
        this.chargeStat.Charging();
    }

    //===========================================Other============================================
    protected override void Respawn()
    {
        base.Respawn();
        this.chargeStat.ActivateCharge();
        this.bodyCollider.enabled = false;
        this.canDespawnByTime = false;
        this.canMove = false;
    }
}
