using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Movement
{
    //==========================================Variable==========================================
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Vector2 moveDir;

    //==========================================Get Set===========================================
    public float MoveSpeed
    {
        get => moveSpeed; 
        set => moveSpeed = value;
    }

    public Vector2 MoveDir
    {
        get => moveDir;
        set => moveDir = value;
    }

    //========================================Constructor=========================================
    public Movement(Rigidbody2D rb)
    {
        this.rb = rb;
    }

    //===========================================Method===========================================
    public virtual void Move()
    {
        this.rb.velocity = this.moveSpeed * this.moveDir.normalized;
    }
}
