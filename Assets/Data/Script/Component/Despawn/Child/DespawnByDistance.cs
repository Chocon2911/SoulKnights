using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DespawnByDistance : BaseDespawn
{
    //==========================================Variable==========================================
    [Header("Despawn By Distance")]
    [SerializeField] private Vector2 despawnRange;
    [SerializeField] private Transform target;

    //==========================================Get Set===========================================
    public Vector2 DespawnRange
    {
        get => despawnRange;
        set => despawnRange = value;
    }

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    //===========================================Unity============================================
    private void FixedUpdate()
    {
        this.Despawn();
    }

    //==========================================Override==========================================
    protected override void Despawn()
    {
        if (this.canDespawn == false || this.target == null || this.despawnRange == null) return;

        float distanceX = Mathf.Abs(this.despawnedObj.position.x - this.target.position.x);
        float distanceY = Mathf.Abs(this.despawnedObj.position.y - this.target.position.y);

        if (distanceX < this.despawnRange.x || distanceY < this.despawnRange.y) return;
        this.spawner.Despawn(this.despawnedObj);
    }
}
