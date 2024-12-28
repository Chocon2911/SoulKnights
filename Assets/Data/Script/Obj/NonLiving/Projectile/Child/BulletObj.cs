using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class BulletObj : BaseProjectile
{
    //==========================================Variable==========================================
    [Header("Bullet Obj")]
    // Unity Component
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D bodyCollider;

    // Obj
    [SerializeField] private BaseMovement movement;

    //==========================================Get Set===========================================
    public Rigidbody2D Rb => rb;
    public CapsuleCollider2D BodyCollider => bodyCollider;
    
    public BaseMovement Movement
    {
        get => movement;
        set => movement = value;
    }

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // Unity Component
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");

        // Child Component
        this.LoadComponent(ref this.movement, transform.Find("Movment"), "LoadMovement()");
        this.movement.Rb = this.rb;
    }

    private void OnEnable()
    {
        this.DefaultStat();
    }

    //==========================================Movement==========================================
    public void PerformMove()
    {
        if (this.movement == null || this.rb == null)
        {
            Debug.LogError("PerformMove() Error", transform.gameObject);
            return;
        }
    }

    //===========================================Other============================================
    private void DefaultStat()
    {
        this.rb.isKinematic = true;
        this.bodyCollider.isTrigger = false;
    }
}
