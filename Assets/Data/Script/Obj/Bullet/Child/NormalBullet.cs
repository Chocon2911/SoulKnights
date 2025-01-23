using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        this.LoadSO(ref this.so, "SO/Obj/Projectile/Bullet/Normal/" + transform.name);
        base.LoadComponents();
    }
}
