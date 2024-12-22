using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerObj : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Player Obj")]
    // Unity Component
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D bodyCollider;
    [SerializeField] private SpriteRenderer model;

    // Component
    [SerializeField] private BaseMovement movement;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        //if (this.rb == null) this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.rb = new Rigidbody2D();
        if (this.bodyCollider == null) this.LoadComponent(ref this.bodyCollider, transform, "LoadBodyCollider()");
        if (this.model == null) this.LoadComponent(ref this.model, transform.Find("Model"), "LoadModel()");
    }

    protected void OnEnable()
    {
        this.DefaultStat();
    }

    private void Update()
    {
        this.movement.Move();
    }

    //==========================================Default===========================================
    private void DefaultStat()
    {
        this.movement = new MoveByKeyboard(this.rb, 10);
    }
}
