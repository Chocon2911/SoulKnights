using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerSO : CharacterSO
{
    //==========================================Variable==========================================
    [Header("Player")]
    //Stat
    [SerializeField] protected int maxMana;
    [SerializeField] protected int maxAmor;

    // Dash Skill
    [SerializeField] protected int dashSkillMC; // Dash Skill Mana Cost
    [SerializeField] protected int dashSkillHC; // Dash Skill Hp Cost
    [SerializeField] protected float dashSkillRT; // Dash Skill Recharge Time
    [SerializeField] protected float dashTime;
    [SerializeField] protected float dashSpeed;

    // Weapon
    [SerializeField] protected int maxWeaponSlot;

    // Amor Regen
    [SerializeField] protected float amorRegenTime;


    //============================================Get=============================================
    // Stat
    public int MaxMana => maxMana;
    public int MaxAmor => maxAmor;

    // Dash Skill
    public float DashSpeed => dashSpeed;
    public int DashSkillMC => dashSkillMC;
    public int DashSkillHC => dashSkillHC;
    public float DashSkillRT => dashSkillRT;
    public float DashTime => dashTime;

    // Weapon
    public int MaxWeaponSlot => maxWeaponSlot;

    // Amor Regen
    public float AmorRegenTime => amorRegenTime;
}
