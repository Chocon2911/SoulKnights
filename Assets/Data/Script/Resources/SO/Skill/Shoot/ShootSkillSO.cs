using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoot", menuName = "SO/Skill/Shoot")]
public class ShootSkillSO : SkillSO
{
    [Header("Shoot")]
    [SerializeField] private float fireRate;
    public float FireRate => fireRate;
}
