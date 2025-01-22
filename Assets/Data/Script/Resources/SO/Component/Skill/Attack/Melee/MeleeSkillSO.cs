using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkillSO : AttackSkillSO
{
    //==========================================Variable==========================================
    [Header("Melee")]
    [SerializeField] private float attackDuration;
    [SerializeField] private int damage;
    [SerializeField] private float forcePower;
    [SerializeField] private float pushBackDuration;

    //============================================Get=============================================
    public float AttackDuration => attackDuration;
    public int Damage => damage;
}
