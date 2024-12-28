using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDespawn
{
    //==========================================Variable==========================================
    [SerializeField] protected Transform despawnedObj;

    //===========================================Method===========================================
    public abstract void Despawn(Spawner spawner);
    protected virtual void DespawnObj(Spawner spawner) { spawner.Despawn(this.despawnedObj); }
}
