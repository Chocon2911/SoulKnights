using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunSkill", menuName = "SO/Skill/Attack/Shoot/Shotgun")]
public class ShotgunSkillSO : ShootSkillSO
{
    //==========================================Variable==========================================
    [Header("Shotgun")]
    [SerializeField] private int bulletCount;
    [SerializeField] private float spreadAngle;

    //============================================Get=============================================
    public int BulletCount => bulletCount;
    public float SpreadAngle => spreadAngle;
}
