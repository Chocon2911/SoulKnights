using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : BaseItem
{
    //==========================================Variable==========================================
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] protected bool canGoThrough;

    //==========================================Delegate==========================================
    System.Action PerformAtBegin = () => { };
    System.Action PerformInProgress = () => { };
    System.Action PerformAtEnd = () => { };
}
