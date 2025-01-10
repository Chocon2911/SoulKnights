using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Player
{
    [Space(25)]
    [Header("//============================================================================================")]
    [Space(25)]
    [Header("===Knight===")]
    [Header("// Character Skill")]
    [SerializeField] private Transform leftArm;
    [SerializeField] private Weapon leftWeapon;
    [SerializeField] private Skill characterSkill;
    [SerializeField] private Cooldown characterSkillExistCD;
    [SerializeField] private float weaponAtkDelay;
    [SerializeField] private int chosenWeaponSlot;
    [SerializeField] private bool canUseCharacterSkill;
    [SerializeField] private bool canCharacterSkillCD;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        this.LoadSO(ref this.so, "SO/Character/Player/Knight/Player");
        base.LoadComponents();
        this.LoadComponent(ref this.leftArm, transform.Find("CharacterSkill").Find("LeftArm"), "LoadLeftArm()");
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.CharacterSkillHandlingFU();
        this.CharacacterSkillWeaponHandling();
        this.CharacterSkillWeaponHolding();
    }

    protected override void Update()
    {
        base.Update();
        this.CharacterSkillHandlingU();
    }

    //======================================Character Skill=======================================
    // =====Skill=====
    // Fixed Update
    private void CharacterSkillHandlingFU()
    {
        if (this.canUseCharacterSkill) this.CharacterSkillPerforming();
        if (this.canCharacterSkillCD) this.CharacterSkillRecharging();
        if (this.characterSkillExistCD.IsReady) this.FinishCharacterSkill();
    }

    // Update
    private void CharacterSkillHandlingU()
    {
        if (InputManager.Instance.ShiftState >= 1 && this.characterSkill.SkillCD.IsReady) this.UseCharacterSkill();
    }

    // Activate Character Skill
    private void UseCharacterSkill()
    {
        this.CloneCurrWeapon();
        this.canCharacterSkillCD = false;
        this.canUseCharacterSkill = true;
        this.characterSkill.SkillCD.ResetStatus();
    }

    // Character Skill Mode end
    private void FinishCharacterSkill()
    {
        this.canCharacterSkillCD = true;
        this.canUseCharacterSkill = false;
        this.characterSkillExistCD.ResetStatus();
        this.leftWeapon.transform.gameObject.SetActive(false);
    }
    
    // Recharge Character Skill
    private void CharacterSkillRecharging()
    {
        this.characterSkill.SkillCD.CoolingDown();
    }

    // During Character Skill mode on
    private void CharacterSkillPerforming()
    {
        this.characterSkillExistCD.CoolingDown();
    }

    // Create Clond of Main Weapon
    private void CloneCurrWeapon()
    {
        if (this.leftWeapon != null) Destroy(this.leftWeapon.gameObject);

        this.chosenWeaponSlot = this.currWeaponSlot;
        Transform mainWeapon = this.weapons[this.chosenWeaponSlot - 1].transform;
        
        Transform tempLeftWeapon = Instantiate(mainWeapon, mainWeapon.position, mainWeapon.rotation, transform.Find("CharacterSkill"));
        tempLeftWeapon.name = "LeftWeapon";

        this.LoadComponent(ref this.leftWeapon, tempLeftWeapon, "LoadLeftWeapon()");
    }

    // =====Weapon=====
    // Handle Weapons of Character Skill
    private void CharacacterSkillWeaponHandling()
    {
        if(!this.canUseCharacterSkill) return;
        else if (this.chosenWeaponSlot != this.currWeaponSlot)
        {
            this.canUseCharacterSkill = false;
            return;
        }

        if(this.leftWeapon is IAttackable)

        {
            IAttackable tempLWeapon = (IAttackable)this.leftWeapon;

            int leftClickState = InputManager.Instance.LeftClickState;
            tempLWeapon.Attack(this, leftClickState);
            StartCoroutine(AttackAfterDelay(tempLWeapon, leftClickState));
        }

        if(this.leftWeapon is ISecondaryAttack)
        {
            ISecondaryAttack tempLWeapon = (ISecondaryAttack)this.leftWeapon;

            int rightClickState = InputManager.Instance.RightClickState;
            tempLWeapon.SecondaryAttack(this, rightClickState);
            StartCoroutine(SecondaryAttackAfterDelay(tempLWeapon, rightClickState));
        }
    }

    private void CharacterSkillWeaponHolding()
    {
        if (!this.canUseCharacterSkill) return;

        Vector2 distance = InputManager.Instance.MousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        this.leftWeapon.HoldingWeapon(this.leftArm, angle);
    }

    // Second Weapon leftClick attack after a period
    private IEnumerator AttackAfterDelay(IAttackable weapon, int leftClickState)
    {
        yield return new WaitForSeconds(weaponAtkDelay);
        weapon.Attack(this, leftClickState);
    }

    // Second Weapon rightClick attack after a period
    private IEnumerator SecondaryAttackAfterDelay(ISecondaryAttack weapon, int rightClickState)
    {
        yield return new WaitForSeconds(weaponAtkDelay);
        weapon.SecondaryAttack(this, rightClickState);
    }

    //==========================================Override==========================================
    protected override void DefaultStat()
    {
        base.DefaultStat();
        if (this.so == null)
        {
            Debug.LogError("KnightSO is null", transform.gameObject);
            return;
        }

        KnightSO knightSO = (KnightSO)this.so;

        Cooldown characterSkillCD = new Cooldown(knightSO.CharacterSkillRT, Time.fixedDeltaTime);
        this.characterSkill = new Skill(knightSO.CharacterSkillMC, knightSO.CharacterSkillHC, characterSkillCD);

        this.characterSkillExistCD = new Cooldown(knightSO.CharacterSkillExistDuration, Time.fixedDeltaTime);
        this.weaponAtkDelay = knightSO.WeaponAtkDelay;
        this.chosenWeaponSlot = this.currWeaponSlot;
        this.canUseCharacterSkill = false;
        this.canCharacterSkillCD = true;
    }
}
