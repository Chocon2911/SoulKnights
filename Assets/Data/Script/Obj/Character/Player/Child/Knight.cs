using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Player
{
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Knight===")]
    [SerializeField] private DualWieldSkill dualWield;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        this.LoadSO(ref this.so, "SO/Character/Player/Knight/Player");
        base.LoadComponents();
    }

    protected override void Update()
    {
        base.Update();
        this.DualWieldUpdate();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.DualWieldFixedUpdate();
    }

    //===========================================Method===========================================
    private void DualWieldUpdate()
    {
        if (InputManager.Instance.ShiftState >= 1 
            && this.weapons.Count > 0
            && this.currWeaponSlot <= this.maxWeaponSlot
            && this.currWeaponSlot >= 1) 
            this.dualWield.UseDualWield(this.weapons[this.currWeaponSlot - 1]);
    }

    private void DualWieldFixedUpdate()
    {
        this.dualWield.DualWieldRecharging();
        this.dualWield.DualWieldPerforming();
        this.dualWield.FinishDualWield();
        this.dualWield.WeaponHolding(transform.position, InputManager.Instance.MousePos);
        this.dualWield.WeaponHandling(this, this, InputManager.Instance.LeftClickState, 
            InputManager.Instance.RightClickState);
    }

    private void DefaultDualWield(KnightSO knightSO)
    {
        Transform leftWeaponHolder = this.transform.Find("CharacterSkill");
        Transform leftArm = leftWeaponHolder.Find("LeftArm");
        Weapon mainWeapon = null;
        this.dualWield = new DualWieldSkill(leftWeaponHolder, leftArm, mainWeapon, 
            knightSO.DualWield, Time.fixedDeltaTime);
        this.dualWield.IsRechargingSkill = true;
        this.dualWield.IsUsingSkill = false;
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        base.DefaultStat();
        KnightSO knightSO = (KnightSO)this.so;
        if (knightSO == null)
        {
            Debug.LogError("KnightSO is null", transform.gameObject);
            return;
        }

        this.DefaultDualWield(knightSO);
    }
}
