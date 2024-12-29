using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDespawn : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Base Despawn")]
    [SerializeField] protected Transform despawnedObj;
    [SerializeField] protected bool canDespawn;
    [SerializeField] protected Spawner spawner;

    //==========================================Get Set===========================================
    public Transform DespawnObj
    {
        get => despawnedObj;
        set => despawnedObj = value;
    }

    public bool CanDespawn
    {
        get => canDespawn;
        set => canDespawn = value;
    }

    public Spawner Spawner
    {
        get => spawner;
        set => spawner = value;
    }

    //===========================================Method===========================================
    protected abstract void Despawn();
}
