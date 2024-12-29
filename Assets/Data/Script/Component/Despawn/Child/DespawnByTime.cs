using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : BaseDespawn
{
    //==========================================Variable==========================================
    [Header("Despawn By Time")]
    [SerializeField] private Cooldown cooldown;

    //==========================================Get Set===========================================
    public Cooldown Cooldown
    {
        get => this.cooldown;
        set => this.cooldown = value;
    }

    //===========================================Unity============================================
    private void FixedUpdate()
    {
        this.cooldown.Counting();
        this.Despawn();
    }

    //==========================================Override==========================================
    protected override void Despawn()
    {
        if (!this.canDespawn || this.cooldown == null || !this.cooldown.IsReady) return;
        this.spawner.Despawn(this.despawnedObj);
    }
}
