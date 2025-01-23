using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUtil
{
    //==========================================Variable==========================================
    private static WeaponUtil instance;
    public static WeaponUtil Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WeaponUtil();
            }
            return instance;
        }
    }

    //========================================Constructor=========================================
    public WeaponUtil()
    {
        if (instance != null)
        {
            Debug.LogError("One WeaponUtil Only");
            return;
        }
    }

    //===========================================Method===========================================
    public void WeaponHolding(TempWeapon weapon, Transform weaponHolder, Vector2 ownerPos, Vector2 targetPos)
    {
        Vector2 distance = targetPos - ownerPos;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        weapon.Holding(weaponHolder, angle);
    }
}
