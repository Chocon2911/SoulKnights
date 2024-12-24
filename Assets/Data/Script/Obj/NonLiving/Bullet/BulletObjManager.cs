using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class BulletObjManager : NonLivingObj
{
    //==========================================Variable==========================================
    [Header("Bullet Obj Manager")]
    // Stat
    [SerializeField] private float flySpeed;
    [SerializeField] private float despawnTime;
    [SerializeField] private float despawnRange;

    // Unity Component
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D bodyCollider;

    // Obj
    [SerializeField] private Movement movement;
    [SerializeField] private List<BaseDespawn> despawn;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // Unity Component
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
    }

    private void OnEnable()
    {
        this.DefaultStat();
        this.movement.Move();
    }

    //===========================================Other============================================
    private void DefaultStat()
    {
        this.flySpeed = 20;
    }
}
