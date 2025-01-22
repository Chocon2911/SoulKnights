using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSO : ScriptableObject
{
    //============================================Stat============================================
    [Header("Skill")]
    [SerializeField] private int manaCost;
    [SerializeField] private int hpCost;
    [SerializeField] private float skillRechargeTime;

    //============================================Get=============================================
    public int ManaCost => manaCost;
    public int HpCost => hpCost;
    public float SkillRechargeTime => skillRechargeTime;
}
