using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : BulletChild
{
    //==========================================Variable==========================================
    [Header("Bullet Despawn")]
    [SerializeField] private DespawnByDistance byDistance;
    [SerializeField] private DespawnByTime byTime;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.byDistance, transform, "LoadDespawnByDistance()");
        this.LoadComponent(ref this.byTime, transform, "LoadDespawnByTime()");
    }

    //===========================================Method===========================================
    public void Handle()
    {
        this.byDistance.CanDespawn = true;
        this.byTime.CanDespawn = true;
    }

    //==========================================Override==========================================
    public override void DefaultStat()
    {
        this.byDistance.DespawnObj = this.bullet.transform;
        this.byDistance.DespawnRange = this.bullet.DespawnRange;
        this.byDistance.Spawner = BulletSpawner.Instance;

        this.byTime.DespawnObj = this.bullet.transform;
        this.byTime.Cooldown = new Cooldown(this.bullet.DespawnTime, Time.fixedDeltaTime);
        this.byTime.Spawner = BulletSpawner.Instance;
    }
}
