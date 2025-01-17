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
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
 
    [Header("Increase Damage")]
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    //==========================================Get Set===========================================
    public ChargeStat ChargeStat { get => this.chargeStat; }

    //===========================================Unity============================================
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    //===========================================Method===========================================
    public virtual void FinishCharge()
    {
        this.chargeStat.FinishCharge();
        this.bodyCollider.enabled = true;
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
    }
}
