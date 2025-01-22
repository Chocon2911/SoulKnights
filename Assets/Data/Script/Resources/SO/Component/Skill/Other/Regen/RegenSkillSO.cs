using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Regen", menuName = "SO/Component/Skill/Other/Regen")]
public class RegenSkillSO : SkillSO
{
    //==========================================Variable==========================================
    [Header("Regen")]
    [SerializeField] private int regenAmountInt;
    [SerializeField] private float regenAmountFloat;
    [SerializeField] private float regenTime;

    //==========================================Get Set===========================================
    public int RegenAmountInt => regenAmountInt;
    public float RegenAmountFloat => regenAmountFloat;
    public float RegenTime => regenTime;
}
