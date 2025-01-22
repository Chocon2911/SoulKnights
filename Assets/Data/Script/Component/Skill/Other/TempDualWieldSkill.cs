using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDualWieldSkill : TempSkill
{
    //==========================================Variable==========================================
    [Header("Dual Wield")]
    [SerializeField] private new DualWieldUser user;
    [SerializeField] private Transform leftArm;
    [SerializeField] private TempWeapon leftWeapon;
    [SerializeField] private Cooldown skillExistCD;
    [SerializeField] private bool isUsingSkill;

    //==========================================Get Set===========================================
    public Transform LeftArm => leftArm;
    public TempWeapon LeftWeapon => leftWeapon;
    public Cooldown SkillExistCD => skillExistCD;
    public bool IsUsingSkill => isUsingSkill;

    //===========================================Skill============================================
    private void CloneWeapon()
    {
        if (this.leftWeapon != null) Destroy(this.leftWeapon.transform.gameObject);

        Transform mainWeaponObj = this.user.GetMainWeapon().transform;
        Transform tempLeftWeapon = Instantiate(mainWeaponObj, mainWeaponObj.position,
            mainWeaponObj.rotation, transform);
        tempLeftWeapon.name = "LeftWeapon";

        this.leftWeapon = transform.Find("LeftWeapon").GetComponent<TempWeapon>();
        this.leftWeapon.SetUser(this.owner.GetComponent<WeaponUser>());
    }

    private void DualWieldPerforming()
    {
        this.SkillExistCD.CoolingDown();
    }

    private void FinishDualWield()
    {
        this.isUsingSkill = false;
        this.skillExistCD.ResetStatus();
        this.skillCD.ResetStatus();

        Transform deletedObj =  this.leftWeapon.transform;
        this.leftWeapon = null;
        Destroy(deletedObj.gameObject);
    }

    //=======================================Weapon Holding=======================================
    private void WeaponHolding()
    {
        WeaponUtil.Instance.WeaponHolding(this.leftWeapon, this.leftArm,
            this.user.GetOwnerPos(), this.user.GetTargetPos());
    }

    //==========================================Override==========================================
    public override void MyLoadComponents()
    {
        base.MyLoadComponents();
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
        this.LoadComponent(ref this.leftArm, transform.Find("LeftArm"), "LoadLeftArm()");
        this.LoadSO(ref this.so, "SO/Skill/Other/DualWield/" + this.owner.name);
        this.LoadComponent(ref this.user, this.owner, "LoadUser()");
    }
    public override void MyFixedUpdate()
    {
        if (this.isUsingSkill) this.DualWieldPerforming();
        if (this.leftWeapon != null && !this.skillExistCD.IsReady) this.WeaponHolding();
        if (this.skillExistCD.IsReady) this.FinishDualWield();
    }

    public override void MyUpdate()
    {
        if (this.user.CanUseSkill(this)
            && this.user.CanUseDualWield()
            && this.skillCD.IsReady) this.UseSkill();
    }

    public override void ResetSkill()
    {
        base.ResetSkill();
        this.skillExistCD.ResetStatus();
        this.isUsingSkill = false;

        if (this.leftWeapon == null) return;
        Transform deletedObj = this.leftWeapon.transform;
        this.leftWeapon = null;
        Destroy(deletedObj.gameObject);
    }

    public override void UseSkill()
    {
        this.CloneWeapon();
        this.isUsingSkill = true;
        this.user.ConsumePower(this);
    }
}
