using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "SO/Skill/Dash")]
public class DashSkillSO : SkillSO
{
    //==========================================Variable==========================================
    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    //============================================Get=============================================
    public float DashSpeed => dashSpeed;
    public float DashTime => dashTime;
}
