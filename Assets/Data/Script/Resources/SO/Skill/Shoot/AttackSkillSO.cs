using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkillSO : SkillSO
{
    [Header("Shoot")]
    [SerializeField] private float attackRate;
    public float AttackRate => attackRate;
}
