using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class BulletObj : BaseProjectile
{
    //==========================================Variable==========================================
    [Header("Bullet Obj")]
    // Stat
    [SerializeField] protected float flySpeed;

    // Unity Component
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected CapsuleCollider2D bodyCollider;

    // Obj
    [SerializeField] protected Movement movement;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // Unity Component
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
    }
}
