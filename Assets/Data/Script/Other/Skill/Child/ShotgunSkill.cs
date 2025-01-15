using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSkill : Skill
{
    //==========================================Variable==========================================
    [Header("Shotgun")]
    [SerializeField] private ShootSkill shootSkill;
    [SerializeField] private int bulletCount;
    [SerializeField] private float spreadAngle;

    //==========================================Get Set===========================================
    public ShootSkill ShootSkill { get => shootSkill; set => shootSkill = value; }
    public int BulletCount { get => bulletCount; set => bulletCount = value; }
    public float SpreadAngle { get => spreadAngle; set => spreadAngle = value; }

    //========================================Constructor=========================================
    public ShotgunSkill(int manaCost, int hpCost, Cooldown skillCD, int bulletCount, float spreadAngle)
        : base(manaCost, hpCost, skillCD)
    {
        this.shootSkill = new ShootSkill(manaCost, hpCost, skillCD);
        this.bulletCount = bulletCount;
        this.spreadAngle = spreadAngle;
    }
}
