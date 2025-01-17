using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDualWieldSkill : TempSkill
{
    //==========================================Variable==========================================
    [Header("Dual Wield")]
    [SerializeField] private DualWieldUser user;
    [SerializeField] private Transform leftWeaponHolder;
    [SerializeField] private Transform leftArm;
    [SerializeField] private Weapon leftWeapon;
    [SerializeField] private Cooldown skillExistCD;
    [SerializeField] private float weaponAtkDelay;
    [SerializeField] private bool isUsingSkill;
    [SerializeField] private bool isRechargingSkill;

    //==========================================Get Set===========================================
    public Transform LeftWeaponHolder => leftWeaponHolder;
    public Transform LeftArm => leftArm;
    public Weapon LeftWeapon => leftWeapon;
    public Cooldown SkillExistCD => skillExistCD;
    public float WeaponAtkDelay => weaponAtkDelay;
    public bool IsUsingSkill => isUsingSkill;
    public bool IsRechargingSkill => isRechargingSkill;

    //===========================================Skill============================================
    private void CloneWeapon()
    {
        if (this.leftWeapon != null) Destroy(this.leftWeapon.transform.gameObject);

        Transform mainWeaponObj = this.user.GetMainWeapon().transform;
        Transform tempLeftWeapon = Instantiate(mainWeaponObj, mainWeaponObj.position,
            mainWeaponObj.rotation, this.leftWeaponHolder);
        tempLeftWeapon.name = "LeftWeapon";
        this.leftWeapon = this.leftWeaponHolder.Find("LeftWeapon").GetComponent<Weapon>();
    }

    private void DualWieldPerforming()
    {
        if (!this.isUsingSkill) return;
        this.SkillExistCD.CoolingDown();
    }

    private void FinishDualWield()
    {
        if (!this.skillExistCD.IsReady) return;

        this.isUsingSkill = false;
        this.skillExistCD.ResetStatus();
        this.skillCD.ResetStatus();

        this.leftWeapon.StopAllCoroutines();
        Destroy(this.leftWeapon.transform.gameObject);
        this.leftWeapon = null;
    }

    //======================================Weapon Handling=======================================


    //=======================================Weapon Holding=======================================


    //==========================================Override==========================================
    public override void MyFixedUpdate()
    {
        
    }

    public override void MyUpdate()
    {
        
    }

    public override void ResetSkill()
    {

    }

    public override void UseSkill()
    {
        if (!this.user.CanUseSkill(this)
            || !this.user.CanUseDualWield()
            || !this.skillCD.IsReady) return;

        this.CloneWeapon();
        this.isUsingSkill = true;
        this.user.ConsumePower(this);
    }
}
