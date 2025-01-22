using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : Movement
{
    //==========================================Variable==========================================
    [Header("Chase Target")]
    [SerializeField] protected new ChaseTargetUser user;
    [SerializeField] protected new ChaseTargetSO so;
    [SerializeField] protected float rotateSpeed;

    //==========================================Get Set===========================================
    public float RotateSpeed { get => rotateSpeed; set => rotateSpeed = value; }

    //==========================================Override==========================================
    protected override void Move()
    {
        MovementUtil.Instance.RotateToTarget(this.user.GetRb(), this.owner, 
            this.user.GetTargetPos(), this.rotateSpeed);
        MovementUtil.Instance.MoveForward(this.user.GetRb(), this.moveSpeed);
    }

    public override void MyLoadComponent()
    {
        base.MyLoadComponent();
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
        this.LoadSO(ref this.so, "SO/Component/Movement/ChaseTarget/" + this.owner.name);
    }

    public override void DefaultStat()
    {
        if (this.so == null)
        {
            Debug.LogError("ChaseTargetSO is null", transform.gameObject);
            return;
        }

        this.rotateSpeed = this.so.RotateSpeed;
    }
}
