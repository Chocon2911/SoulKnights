using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovement : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float moveSpeed;

    //==========================================Get Set===========================================
    public Rigidbody2D Rb
    {
        get => rb;
        set => rb = value;
    }

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    //===========================================Method===========================================
    public void AddVelocity(Vector2 vel)
    {
        rb.velocity += vel;
    }

    public void Move()
    {
        if (this.rb == null)
        {
            Debug.LogError("Rb is null", transform.gameObject);
            return;
        }

        this.rb.velocity += this.moveSpeed * this.GetDir();
    }

    //==========================================Abstract==========================================
    protected abstract Vector2 GetDir();
}
