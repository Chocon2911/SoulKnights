using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : Despawner
{
    //==========================================Variable==========================================
    [Header("Despawn By Time")]
    [SerializeField] private Cooldown despawnCD;

    //==========================================Get Set===========================================
    public Cooldown DespawnCD { get => despawnCD; set => despawnCD = value; }

    //==========================================Override==========================================
    protected override void Despawn()
    {
        if (!this.despawnCD.IsReady) this.despawnCD.CoolingDown();
        else
        {
            this.ResetDespawn();
            this.DespawnObj();
        }
    }

    public override void ResetDespawn()
    {
        base.ResetDespawn();
        this.despawnCD.ResetStatus();
    }
}
