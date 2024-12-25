using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class BulletObj : NonLivingObj
{
    //==========================================Variable==========================================
    [Header("Bullet Obj Manager")]
    // Unity Component
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D bodyCollider;

    // Obj
    [SerializeField] private Movement movement;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // Unity Component
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
    }
}
