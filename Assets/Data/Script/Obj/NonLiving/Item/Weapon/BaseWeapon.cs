using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseWeapon : BaseItem
{
    //==========================================Variable==========================================
    [SerializeField] protected int ManaConsumeAmount;
    [SerializeField] protected int damage;
}
