using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "SO/Character/Player")]
public class PlayerSO : CharacterSO
{
    //==========================================Variable==========================================
    [Header("Player")]
    //Stat
    [SerializeField] protected int maxMana;
    [SerializeField] protected int maxAmor;

    // Weapon
    [SerializeField] protected int maxWeaponSlot;

    //============================================Get=============================================
    // Stat
    public int MaxMana => maxMana;
    public int MaxAmor => maxAmor;

    // Weapon
    public int MaxWeaponSlot => maxWeaponSlot;
}
