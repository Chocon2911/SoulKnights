using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkillSO : SkillSO
{
    //==========================================Variable==========================================
    [Header("Attack")]
    [SerializeField] private float forcePower;
    [SerializeField] private float pushBackDuration;
    [SerializeField] private float attackRate;

    //==========================================Variable==========================================
    public float ForcePower => forcePower;
    public float PushBackDuration => pushBackDuration;
    public float AttackRate => attackRate;
}
