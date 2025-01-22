using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AdvanceShotgun", menuName = "SO/Skill/Attack/Shoot/AdvanceShotgun")]
public class AdvanceShotgunSkillSO : ShootSkillSO
{
    //==========================================Variable==========================================
    [Header("Advance Shotgun")]
    [SerializeField] private List<float> bulletAngles;

    //============================================Get=============================================
    public List<float> BulletAngles => bulletAngles;
}
