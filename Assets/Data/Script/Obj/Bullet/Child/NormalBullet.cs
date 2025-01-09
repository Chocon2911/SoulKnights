using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        this.LoadSO(ref this.so, "SO/Projectile/Bullet/NormalBullet");
        base.LoadComponents();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Move();
    }

    //==========================================Movement==========================================
    private void Move()
    {
        if (!this.canMove) return;
        MovementUtil.Instance.MoveForward(this.rb, this.moveSpeed);   
    }

    //==========================================Override==========================================
    protected override void OnColliding(Transform collidedObj)
    {
        throw new System.NotImplementedException();
    }
}
