using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSkillSO : AttackSkillSO
{
    //==========================================Variable==========================================
    [Header("Shotgun")]
    [SerializeField] private int bulletCount;
    [SerializeField] private float spreadAngle;

    //============================================Get=============================================
    public int BulletCount => bulletCount;
    public float SpreadAngle => spreadAngle;
}
