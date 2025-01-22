using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "SO/Equipment/Weapon")]
public class WeaponSO : ObjSO
{
    //==========================================Variable==========================================
    [Header("Weapon")]
    [SerializeField] private float holdRad;

    //============================================Get=============================================
    public float HoldRad => holdRad;
}
