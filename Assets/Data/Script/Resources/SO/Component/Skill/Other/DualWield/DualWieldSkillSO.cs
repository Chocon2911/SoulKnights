using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DualWield", menuName = "SO/Skill/DualWield")]
public class DualWieldSkillSO : SkillSO
{
    //==========================================Variable==========================================
    [Header("DualWield")]
    [SerializeField] private float skillExistTime;
    [SerializeField] private float weaponAtkDelay;

    //==========================================Get Set===========================================
    public float SkillExistTime => skillExistTime;
    public float WeaponAtkDelay => weaponAtkDelay;
}
