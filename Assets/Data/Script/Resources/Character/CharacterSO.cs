using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterSO : ObjSO
{
    //==========================================Variable==========================================
    [Header("Character")]
    [SerializeField] protected int maxHp;
    [SerializeField] protected float moveSpeed;

    //============================================Get=============================================
    public int MaxHp => maxHp;
    public float MoveSpeed => moveSpeed;
}
