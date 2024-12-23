using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerObjManager : LivingObj
{
    //==========================================Variable==========================================
    [Header("Player Obj")]
    // Stat
    [SerializeField] private float moveSpeed;

    // Unity Component
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D bodyCollider;

    // Child Component
    [SerializeField] private PlayerMovement movement; 

    //==========================================Get Set===========================================
    // Stat
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    
    // Unity Component
    public Rigidbody2D Rb => rb;
    public CapsuleCollider2D BodyCollider => bodyCollider;

    // Child Component
    public PlayerMovement Movement => movement;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // Unity Component
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
        this.LoadComponent(ref this.model, transform.Find("Model"), "LoadModel()");

        // Child Component
        this.LoadComponent(ref this.movement, transform.Find("Movement"), "LoadMovement()");
    }

    private void OnEnable()
    {
        this.DefaultStat();
    }

    //===========================================Other============================================
    private void DefaultStat()
    {
        this.moveSpeed = 10;
        this.movement.DefaultStat();
    }
}
