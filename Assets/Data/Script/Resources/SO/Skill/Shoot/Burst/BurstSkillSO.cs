using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstSkillSO : AttackSkillSO
{
    //==========================================Variable==========================================
    [Header("Burst")]
    [SerializeField] private int burstCount;
    [SerializeField] private float burstInterval;

    //============================================Get=============================================
    public int BurstCount => burstCount;
    public float BurstInterval => burstInterval;
}
