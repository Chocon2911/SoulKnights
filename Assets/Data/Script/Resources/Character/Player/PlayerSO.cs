using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "SO/Character/Player")]
public class PlayerSO : CharacterSO
{
    //==========================================Variable==========================================
    [Header("Player")]
    [SerializeField] protected int maxMana;
    [SerializeField] protected int maxAmor;
    [SerializeField] protected float dashSpeed;

    //============================================Get=============================================
    public int MaxMana => maxMana;
    public int MaxAmor => maxAmor;
    public float DashSpeed => dashSpeed;
}
