using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : BaseObj
{
    //==========================================Variable==========================================
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Weapon===")]
    [SerializeField] protected float holdRad;

    //==========================================Get Set===========================================
    public float HoldRad
    {
        get => holdRad;
        set => holdRad = value;
    }

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.DefaultStat();
    }

    //===========================================Method===========================================
    public virtual void HoldingWeapon(Transform owner, float angle)
    {
        Vector2 pos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * this.holdRad;
        transform.localPosition = pos;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        base.DefaultStat();
        WeaponSO weaponSO = (WeaponSO)this.so;
        if (weaponSO == null)
        {
            Debug.LogError("WeaponSO is null", transform.gameObject);
            return;
        }

        this.holdRad = weaponSO.HoldRad;
    }
}
