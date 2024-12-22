using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChaseTarget : BaseMovement
{
    //==========================================Variable==========================================
    [SerializeField] protected Transform currObj;
    [SerializeField] protected Transform target;

    //===========================================Method===========================================
    private void UpdateDir()
    {
        if (this.currObj == null || this.target == null) this.moveDir = Vector2.zero;
        this.moveDir = this.currObj.position - this.target.position;
    }

    //==========================================Override==========================================
    public override void Move()
    {
        this.UpdateDir();
        base.Move();
    }
}
