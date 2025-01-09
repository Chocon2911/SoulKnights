using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "SO/Character/Player/Knight")]
public class KnightSO : PlayerSO
{
    //==========================================Variable==========================================
    [Header("Knight")]
    [SerializeField] private int characterSkillMC; // Character Skill Mana Cost
    [SerializeField] private int characterSkillHC; // Character Skill Hp Cost 
    [SerializeField] private float characterSkillRT; // Character Skill Recharge Time
    [SerializeField] private float characterSkillExistDuration; // Character Skill Exist Duration
    [SerializeField] private float weaponAtkDelay;

    //============================================Get=============================================
    public int CharacterSkillMC => characterSkillMC;
    public int CharacterSkillHC => characterSkillHC;
    public float CharacterSkillRT => characterSkillRT;
    public float CharacterSkillExistDuration => characterSkillExistDuration;
    public float WeaponAtkDelay => weaponAtkDelay;
}
