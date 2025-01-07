using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : BaseObj
{
    //==========================================Variable==========================================
    [Header("Weapon")]
    [SerializeField] protected float holdRad;

    //==========================================Get Set===========================================
    public float HoldRad
    {
        get => holdRad;
        set => holdRad = value;
    }

    //===========================================Method===========================================
    public virtual void HoldingWeapon(Transform owner, float angle)
    {
        Vector2 pos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * this.holdRad;
        transform.localPosition = pos;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
