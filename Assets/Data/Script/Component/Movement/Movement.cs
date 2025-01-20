using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Movement")]
    [SerializeField] protected MovementUser user;
    [SerializeField] protected Transform owner;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected bool canMove;

    //==========================================Get Set===========================================
    public Transform Owner { get => owner; set => owner = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public bool CanMove { get => canMove; set => canMove = value; }

    //==========================================Abstract==========================================
    public virtual void MyLoadComponent()
    {
        // For Override
    }

    public virtual void MyUpdate()
    {
        // For Override
    }

    public virtual void MyFixedUpdate()
    {
        if (this.canMove) this.Move();
    }

    public virtual void ResetMovement()
    {
        // For Override
    }

    protected abstract void Move();
}
