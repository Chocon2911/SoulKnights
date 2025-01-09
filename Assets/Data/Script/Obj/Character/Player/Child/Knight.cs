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
    [SerializeField] private Weapon leftWeapon; 
    [SerializeField] private Weapon rightWeapon;
    [SerializeField] private Skill characterSkill;
    [SerializeField] private Cooldown characterSkillExistCD;
    [SerializeField] private float weaponAtkDelay;
    [SerializeField] private int chosenWeaponSlot;
    [SerializeField] private bool canUseCS;
    [SerializeField] private bool canCharacterSkillCD;

    //===========================================Unity============================================
    protected override void LoadComponents()
    {
        this.LoadSO(ref this.so, "SO/Character/Player/Knight/Player");
        base.LoadComponents();
    }

    //======================================Character Skill=======================================
    private void UseCharacterSkill()
    {
        if (!this.characterSkill.SkillCD.IsReady) return;
        this.canCharacterSkillCD = false;
        this.canUseCS = true;
        // Load Right Weapon
        // Load Left Weapon
    }

    private void FinishCharacterSkill()
    {
        this.canCharacterSkillCD = true;
        this.canUseCS = false;
    }
    
    private void CharacterSkillRecharging()
    {
        if (!this.canCharacterSkillCD || this.characterSkill.SkillCD.IsReady) return;
        this.characterSkill.SkillCD.CoolingDown();
    }

    private void CharacterSkillHandling()
    {
        if (!this.canUseCS) return;
        this.characterSkillExistCD.CoolingDown();
    }

    private void WeaponHandlingCS()
    {
        if(!this.canUseCS) return;
        
        else if (this.chosenWeaponSlot != this.currWeaponSlot)
        {
            this.canUseCS = false;
            return;
        }

        if(this.leftWeapon is IAttackable)

        {
            IAttackable tempLWeapon = (IAttackable)this.leftWeapon;
            IAttackable tempRWeapon = (IAttackable)this.rightWeapon;

            int leftClickState = InputManager.Instance.LeftClickState;
            tempLWeapon.Attack(this, leftClickState);
            StartCoroutine(AttackAfterDelay(tempRWeapon, leftClickState));
        }

        if(this.leftWeapon is IAttackable)
        {
            ISecondaryAttack tempLWeapon = (ISecondaryAttack)this.leftWeapon;
            ISecondaryAttack tempRWeapon = (ISecondaryAttack)this.rightWeapon;

            int rightClickState = InputManager.Instance.RightClickState;
            tempLWeapon.SecondaryAttack(this, rightClickState);
            StartCoroutine(SecondaryAttackAfterDelay(tempRWeapon, rightClickState));
        }
    }

    private IEnumerator AttackAfterDelay(IAttackable weapon, int leftClickState)
    {
        yield return new WaitForSeconds(weaponAtkDelay);
        weapon.Attack(this, leftClickState);
    }

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
        this.chosenWeaponSlot = knightSO.MaxWeaponSlot;
        this.canUseCS = false;
        this.canCharacterSkillCD = true;
    }
}
