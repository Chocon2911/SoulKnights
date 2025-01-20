using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTargetByCollide : Movement
{
    //==========================================Variable==========================================
    [Header("Chase Target By Collide")]
    [SerializeField] private CircleCollider2D bodyCollider;

    //==========================================Get Set===========================================
    public CircleCollider2D BodyCollider { get => bodyCollider; set => bodyCollider = value; }

    //===========================================Method===========================================
    protected override void Move()
    {
        throw new System.NotImplementedException();
    }
}
