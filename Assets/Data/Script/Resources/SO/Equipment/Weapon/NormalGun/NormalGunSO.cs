using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "M4", menuName = "SO/Equipment/Weapon/NormalGun")]
public class NormalGunSO : WeaponSO
{
    //==========================================Variable==========================================
    [Header("NormalGun")]
    [SerializeField] private ShootSkillSO skill;
    [SerializeField] private Transform bullet;

    //============================================Get=============================================
    public ShootSkillSO Skill => skill;
    public Transform Bullet => bullet;
}
