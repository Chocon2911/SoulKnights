using System.Collections;
using UnityEngine;

public class ShootSkillSO : SkillSO
{
    //==========================================Variable==========================================
    [Header("Shoot")]
    [SerializeField] private Transform bullet;
    [SerializeField] private float fireRate;

    //============================================Get=============================================
    public Transform Bullet => bullet;
    public float FireRate => fireRate;
}
