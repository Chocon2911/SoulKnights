using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerObj : LivingObj
{
    //==========================================Variable==========================================
    [Header("Player Obj")]
    // Stat
    [SerializeField] protected float moveSpeed;

    // Unity Component
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected CapsuleCollider2D bodyCollider;

    // Child Component
    [SerializeField] protected PlayerSO so;
    [SerializeField] protected PlayerMovement movement;
    [SerializeField] protected PlayerWeapon weapon;
    [SerializeField] protected PlayerStat stat;

    //==========================================Get Set===========================================
    // Stat
    public float MoveSpeed => this.moveSpeed;

    // Unity Component
    public Rigidbody2D Rb => this.rb;
    public CapsuleCollider2D BodyCollider => this.bodyCollider;

    // Child Component
    public PlayerMovement Movement => this.movement;
    public PlayerWeapon Weapon => this.weapon;
    public PlayerStat Stat => this.stat;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // SO
        string filePath = "SO/Living/Player/" + transform.name;
        this.LoadSO<PlayerSO>(ref this.so, filePath);

        // Unity Component
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
        this.LoadComponent(ref this.model, transform.Find("Model"), "LoadModel()");

        // Child Component
        this.LoadComponent(ref this.movement, transform.Find("Movement"), "LoadMovement()");
        this.LoadComponent(ref this.weapon, transform.Find("Weapon"), "LoadWeapon()");
        this.LoadComponent(ref this.stat, transform.Find("Stat"), "LoadStat()");
    }

    private void Update()
    {
        this.movement.Handling();
    }

    private void OnEnable()
    {
        this.LoadComponents();
        this.DefaultStat();

        // Child Component
        this.movement.DefaultStat();
    }

    //===========================================Other============================================
    private void DefaultStat()
    {
        if (this.so == null)
        {
            Debug.LogError("SO is null", transform.gameObject);
            return;
        }

        // SO
        this.objName = this.so.name;
        this.moveSpeed = this.so.MoveSpeed;
    }
}
