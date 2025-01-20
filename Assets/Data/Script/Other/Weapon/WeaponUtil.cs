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
    public void WeaponHandling(Weapon weapon, int leftClickState, int rightClickState, 
        HpReceiver hpRecv, ManaReceiver manaRecv)
    {
        if (weapon is IAttackable)
        {
            IAttackable firstAtk = (IAttackable)weapon;
            firstAtk.Attack(hpRecv, manaRecv, leftClickState);
        }

        if (weapon is ISecondaryAttack)
        {
            ISecondaryAttack secondaryAtk = (ISecondaryAttack)weapon;
            secondaryAtk.SecondaryAttack(hpRecv, manaRecv, rightClickState);
        }
    }

    public void WeaponHolding(Weapon weapon, Transform weaponHolder, Vector2 ownerPos, Vector2 targetPos)
    {
        Vector2 distance = targetPos - ownerPos;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        weapon.HoldingWeapon(weaponHolder, angle);
    }

    public void WeaponHolding(TempWeapon weapon, Transform weaponHolder, Vector2 ownerPos, Vector2 targetPos)
    {
        Vector2 distance = targetPos - ownerPos;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        weapon.Holding(weaponHolder, angle);
    }
}
