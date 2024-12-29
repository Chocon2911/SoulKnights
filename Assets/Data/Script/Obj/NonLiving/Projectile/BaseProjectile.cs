using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : BaseItem
{
    //==========================================Variable==========================================
    [SerializeField] protected Collider2D bodyCollider;
    [SerializeField] protected bool canGoThrough;

    //==========================================Get Set===========================================
    public Collider2D BodyCollider => bodyCollider;
    public bool CanGoThrough => canGoThrough;

    //==========================================Delegate==========================================
    System.Action PerformAtBegin = () => { };
    System.Action PerformInProgress = () => { };
    System.Action PerformAtEnd = () => { };
}
