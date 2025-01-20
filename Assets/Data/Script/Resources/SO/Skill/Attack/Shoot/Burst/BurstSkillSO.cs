using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurstSkill", menuName = "SO/Skill/Attack/Shoot/Burst")]
public class BurstSkillSO : ShootSkillSO
{
    //==========================================Variable==========================================
    [Header("Burst")]
    [SerializeField] private int burstCount;
    [SerializeField] private float burstInterval;

    //============================================Get=============================================
    public int BurstCount => burstCount;
    public float BurstInterval => burstInterval;
}
