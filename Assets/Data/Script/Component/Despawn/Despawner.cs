using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawner : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Despawner")]
    [SerializeField] protected Transform owner;
    [SerializeField] protected DespawnUser user;
    [SerializeField] private bool canDespawn;

    //==========================================Get Set===========================================
    public Transform Owner { get => owner; set => owner = value; }
    public virtual DespawnUser User { get => user; set => user = value; }
    public bool CanDespawn { get => canDespawn; set => canDespawn = value; }

    //===========================================Method===========================================
    protected virtual void DespawnObj()
    {
        DespawnUtil.Instance.Despawn(this.owner, this.user.GetSpawner());
    }

    //===========================================Other============================================
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
        if (this.canDespawn) this.DespawnObj();
    }

    public virtual void ResetDespawn()
    {
        this.canDespawn = true;
    }

    //==========================================Abstract==========================================
    protected abstract void Despawn();
}
