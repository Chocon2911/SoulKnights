using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : BulletChild
{
    //==========================================Variable==========================================
    [SerializeField] private MoveForward moveForward;

    //===========================================Unity============================================
    protected override void LoadComponents() 
    { 
        base.LoadComponents();
        this.LoadComponent(ref this.moveForward, transform, "LoadMoveForward()");
    }

    //==========================================Override==========================================
    public override void DefaultStat()
    {
        this.moveForward.Rb = this.bullet.Rb;
        this.moveForward.MoveSpeed = this.bullet.MoveSpeed;
    }
}
