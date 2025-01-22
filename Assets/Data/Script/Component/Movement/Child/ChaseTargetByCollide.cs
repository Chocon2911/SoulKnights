using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseTargetByCollide : ChaseTarget
{
    //==========================================Variable==========================================
    [Header("Chase Target By Collide")]
    [SerializeField] private IdentifyObjByCollide idenByCollide;

    //==========================================Get Set===========================================
    public IdentifyObjByCollide IdenByCollide { get => idenByCollide; set => idenByCollide = value; }

    //===========================================Method===========================================
    protected override void Move()
    {
        if (this.idenByCollide.Target != null)
        {
            MovementUtil.Instance.RotateToTarget(this.user.GetRb(), this.owner, 
                this.idenByCollide.Target.position, this.rotateSpeed);
        }

        MovementUtil.Instance.MoveForward(this.user.GetRb(), this.moveSpeed);
    }

    public override void MyLoadComponent()
    {
        base.MyLoadComponent();
        this.LoadSO(ref this.so, "SO/Component/Movement/ChaseTarget/ByCollide/" + this.owner.name);
    }

    public override void DefaultStat()
    {
        base.DefaultStat();
        if (this.so == null)
        {
            Debug.LogError("ChaseTargetByCollideSO is null", transform.gameObject);
            return;
        }

        // SO for IdentifyObjByCollide
    }
}
