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
    [SerializeField] protected DashSkillSO dashSkillSO;

    // Weapon
    [SerializeField] protected int maxWeaponSlot;

    // Amor Regen
    [SerializeField] protected RegenSkillSO amorRegenSO;

    //============================================Get=============================================
    // Stat
    public int MaxMana => maxMana;
    public int MaxAmor => maxAmor;

    // Dash Skill
    public DashSkillSO DashSkillSO => dashSkillSO;

    // Weapon
    public int MaxWeaponSlot => maxWeaponSlot;

    // Amor Regen
    public RegenSkillSO AmorRegenSO => amorRegenSO;
}
