using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseCharacter : BaseObj
{
    //==========================================Variable==========================================
    [Header("Character")]
    // Stat
    [SerializeField] protected int maxHp;
    [SerializeField] protected int hp;
    [SerializeField] protected float moveSpeed;
                     
    // Component     
    [SerializeField] protected CapsuleCollider2D bodyCollider;
    [SerializeField] protected Rigidbody2D rb;

    //==========================================Get Set===========================================
    // Stat
    public int MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }

    public int Hp
    {
        get => hp;
    }

    public float MoveSpeed
    {
        get => moveSpeed; 
        set => moveSpeed = value;
    }

    // Component
    public CapsuleCollider2D BodyCollider
    {
        get => bodyCollider; 
        set => bodyCollider = value;
    }

    public Rigidbody2D Rb
    {
        get => rb;
        set => rb = value;
    }

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
    }
}
