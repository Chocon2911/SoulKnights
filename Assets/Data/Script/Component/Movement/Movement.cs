using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Movement")]
    [SerializeField] protected MovementSO so;
    [SerializeField] protected MovementUser user;
    [SerializeField] protected Transform owner;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected bool canMove;

    //==========================================Get Set===========================================
    public Transform Owner { get => owner; set => owner = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public bool CanMove { get => canMove; set => canMove = value; }

    //===========================================Other============================================
    public virtual void MyLoadComponent()
    {
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
        this.LoadSO(ref this.so, "SO/Component/Movement/" + this.owner.name);
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

    public virtual void DefaultStat()
    {
        if (this.so != null)
        {
            Debug.LogError("MovementSO is null", transform.gameObject);
            return;
        }

        this.moveSpeed = this.so.MoveSpeed;
    }

    //==========================================Abstract==========================================
    protected abstract void Move();
}
