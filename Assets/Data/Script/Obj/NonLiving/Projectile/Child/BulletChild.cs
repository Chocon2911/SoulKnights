using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChild : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Bullet Child")]
    [SerializeField] protected Bullet bullet;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.bullet, transform.parent, "LoadBullet()");
    }

    //==========================================Abstract==========================================
    public virtual void DefaultStat()
    {
        //if (this.bullet == null || this.bullet.so == null)
        //{
        //    Debug.LogError("SO is null", transform.gameObject);
        //    return;
        //}
    }
}
