using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : Movement
{
    //==========================================Variable==========================================
    [Header("Chase Target")]
    [SerializeField] private new ChaseTargetUser user;
    [SerializeField] private float rotateSpeed;

    //==========================================Get Set===========================================
    public float RotateSpeed { get => rotateSpeed; set => rotateSpeed = value; }

    //===========================================Method===========================================
    private void RotateToTarget()
    {
        Vector2 distance = (Vector2)this.owner.position - this.user.GetTargetPos();
        Quaternion finalRot = Quaternion.Euler(distance);
        Quaternion newRot = Quaternion.RotateTowards(this.owner.rotation, finalRot,
            this.rotateSpeed * Time.fixedDeltaTime);

        this.user.GetRb().MoveRotation(newRot);
    }

    //==========================================Override==========================================
    protected override void Move()
    {
        this.RotateToTarget();
        MovementUtil.Instance.MoveForward(this.user.GetRb(), this.moveSpeed);
    }
}
