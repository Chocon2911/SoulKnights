using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawner
{
    //==========================================Variable==========================================
    [Header("Despawn By Distance")]
    [SerializeField] private new DespawnByDistanceUser user;
    [SerializeField] private float despawnDistance;
    [SerializeField] private bool canDespawn;

    //==========================================Get Set===========================================
    public float DespawnDistance { get => despawnDistance; set => despawnDistance = value; }

    //===========================================Method===========================================
    protected override void Despawn()
    {
        if (this.GetCanDespawn()) this.DespawnObj();
    }

    private bool GetCanDespawn()
    {
        float distance = Vector2.Distance(this.owner.position, this.user.GetTargetPos());
        
        if (Mathf.Abs(distance) >= this.despawnDistance) return true;
        else return false;
    }

    public override void MyLoadComponent()
    {
        base.MyLoadComponent();
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
    }
}
