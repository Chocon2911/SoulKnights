using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class BulletObjManager : NonLivingObj
{
    //==========================================Variable==========================================
    [Header("Bueet Obj Manager")]
    // Stat
    [SerializeField] private float flySpeed;

    // Unity Component
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D bodyCollider;

    // Obj
    [SerializeField] private BaseMovement movement;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // Unity Component
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider");
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
        this.movement = new MoveForward(this.rb, this.flySpeed, transform.rotation.eulerAngles.z);
    }
}
