using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseMovement
{
    //==========================================Variable==========================================
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Vector2 moveDir;

    //===========================================Method===========================================
    public virtual void Move()
    {
        this.rb.velocity = this.moveSpeed * this.moveDir.normalized;
    }
}
