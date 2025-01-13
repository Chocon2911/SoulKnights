using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DualWieldSkill : Skill
{
    //==========================================Variable==========================================
    [Header("Dual Wield")]
    [SerializeField] private Transform leftWeaponHolder;
    [SerializeField] private Transform leftArm;
    [SerializeField] private Weapon mainWeapon;
    [SerializeField] private Weapon leftWeapon;
    [SerializeField] private Cooldown skillExistCD;
    [SerializeField] private float weaponAtkDelay;
    [SerializeField] private bool isUsingSkill;
    [SerializeField] private bool isRechargingSkill;
    [SerializeField] private bool hasfirstAttackDone;
    [SerializeField] private bool hasSecondAttackDone;

    //==========================================Get Set===========================================
    public Cooldown SkillExistCD
    {
        get => this.SkillExistCD;
    }
    
    public bool IsUsingSkill
    {
        set => this.isUsingSkill = value;
    }

    public bool IsRechargingSkill
    {
        set => this.isRechargingSkill = value;
    }

    //========================================Constructor=========================================
    public DualWieldSkill(int manaCost, int hpCost, Cooldown skillCD, Transform leftWeaponHolder, 
        Transform leftArm, Weapon mainWeapon, Cooldown characterSkillExistCD, float weaponAtkDelay) : 
        base(manaCost, hpCost, skillCD)
    {
        this.leftWeaponHolder = leftWeaponHolder;
        this.leftArm = leftArm;
        this.mainWeapon = mainWeapon;
        this.skillExistCD = characterSkillExistCD;
        this.weaponAtkDelay = weaponAtkDelay;
        this.isUsingSkill = false;
        this.isRechargingSkill = false;
    }

    public DualWieldSkill(Transform leftWeaponHolder, Transform leftArm, Weapon mainWeapon, 
        DualWieldSkillSO so, float waitTime) : 
        base(so.ManaCost, so.HpCost, new Cooldown(so.SkillRechargeTime, waitTime))
    {
        
        this.leftWeaponHolder = leftWeaponHolder;
        this.leftArm = leftArm;
        this.mainWeapon = mainWeapon;
        this.skillExistCD = new Cooldown(so.SkillExistTime, waitTime);
        this.weaponAtkDelay = so.WeaponAtkDelay;
        this.isUsingSkill = false;
        this.isRechargingSkill = false;
    }

    //======================================Character Skill=======================================
    // Activate Character Skill
    public void UseDualWield(Weapon mainWeapon)
    {
        if (!this.skillCD.IsReady) return;

        this.mainWeapon = mainWeapon;
        this.CloneCurrWeapon();
        this.isRechargingSkill = false;
        this.isUsingSkill = true;
        this.skillCD.ResetStatus();
    }

    // Character Skill Mode end
    public void FinishDualWield()
    {
        if (!this.skillExistCD.IsReady) return;

        this.isRechargingSkill = true;
        this.isUsingSkill = false;
        this.skillExistCD.ResetStatus();

        this.leftWeapon.StopAllCoroutines();
        UnityEngine.Object.Destroy(this.leftWeapon.transform.gameObject);
        this.leftWeapon = null;
    }
    
    // Recharge Character Skill
    public void DualWieldRecharging()
    {
        if (!this.isRechargingSkill) return;
        this.skillCD.CoolingDown();
    }

    // During Character Skill mode on
    public void DualWieldPerforming()
    {
        if (!this.isUsingSkill) return;
        this.skillExistCD.CoolingDown();
    }

    // Create Clond of Main Weapon
    private void CloneCurrWeapon()
    {
        if (this.leftWeapon != null) UnityEngine.Object.Destroy(this.leftWeapon.transform.gameObject);

        Transform mainWeaponObj = this.mainWeapon.transform;
        Transform tempLeftWeapon = UnityEngine.Object.Instantiate(mainWeaponObj, mainWeaponObj.position, 
            mainWeaponObj.rotation, this.leftWeaponHolder);
        tempLeftWeapon.name = "LeftWeapon";
        this.leftWeapon = this.leftWeaponHolder.Find("LeftWeapon").GetComponent<Weapon>();
    }

    //===========================================Weapon===========================================
    public void WeaponHandling(HpReceiver hpRecv, ManaReceiver manaRecv, int leftClickState, int rightClickState)
    {
        if (this.skillExistCD.IsReady) return;

        if (this.leftWeapon is IAttackable)
        {
            IAttackable tempLWeapon = (IAttackable)this.leftWeapon;
            this.leftWeapon.StartCoroutine(AttackAfterDelay(hpRecv, manaRecv, tempLWeapon, leftClickState));
        }

        if (this.leftWeapon is ISecondaryAttack)
        {
            ISecondaryAttack tempLWeapon = (ISecondaryAttack)this.leftWeapon;
            this.leftWeapon.StartCoroutine(SecondaryAttackAfterDelay(hpRecv, manaRecv, tempLWeapon, rightClickState));
        }
    }

    public void WeaponHolding(Vector2 ownerPos, Vector2 target)
    {
        Vector2 dir = target - ownerPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        this.WeaponHolding(angle);
    }

    public void WeaponHolding(float angle)
    {
        if (!this.isUsingSkill) return;
        this.leftWeapon.HoldingWeapon(this.leftArm, angle);
    }

    // Second Weapon leftClick attack after a period
    private IEnumerator AttackAfterDelay(HpReceiver hpRecv, ManaReceiver manaRecv, IAttackable weapon, int leftClickState)
    {
        yield return new WaitForSeconds(weaponAtkDelay);
        weapon.Attack(hpRecv, manaRecv, leftClickState);
    }

    // Second Weapon rightClick attack after a period
    private IEnumerator SecondaryAttackAfterDelay(HpReceiver hpRecv, ManaReceiver manaRecv, ISecondaryAttack weapon, int rightClickState)
    {
        yield return new WaitForSeconds(weaponAtkDelay);
        weapon.SecondaryAttack(hpRecv, manaRecv, rightClickState);
    }
}
