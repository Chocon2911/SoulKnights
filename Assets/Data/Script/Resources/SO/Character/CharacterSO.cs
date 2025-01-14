using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterSO : ObjSO
{
    [Header("Character")]
    [SerializeField] protected int maxHp;
    public int MaxHp => maxHp;
}
