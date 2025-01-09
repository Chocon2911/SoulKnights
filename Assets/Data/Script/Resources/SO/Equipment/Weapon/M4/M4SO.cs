using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "M4", menuName = "SO/Equipment/Weapon/M4")]
public class M4SO : WeaponSO
{
    //==========================================Variable==========================================
    [Header("M4")]
    [SerializeField] private float fireRate;
    [SerializeField] private Transform bullet;

    //============================================Get=============================================
    public float FireRate => fireRate;
    public Transform Bullet => bullet;
}
