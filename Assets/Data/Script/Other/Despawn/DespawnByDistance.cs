using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : BaseDespawn
{
    //==========================================Variable==========================================
    [SerializeField] private Vector2 despawnRange;
    [SerializeField] private Transform target;

    //===========================================Method===========================================
    private bool canDespawn()
    {
        float distanceX = Mathf.Abs(this.despawnedObj.position.x - this.target.position.x);
        float distanceY = Mathf.Abs(this.despawnedObj.position.y - this.target.position.y);

        if (distanceX > this.despawnRange.x || distanceY > this.despawnRange.y) return true;
        return false;
    }

    //==========================================Override==========================================
    public override void Despawn(Spawner spawner)
    {
        if (this.target == null
        || this.despawnRange == null
        || !this.canDespawn()) return;

        this.DespawnObj(spawner);
    }
}
