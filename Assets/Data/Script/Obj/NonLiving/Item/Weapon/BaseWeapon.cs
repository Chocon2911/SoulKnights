using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseWeapon : BaseItem
{
    //==========================================Variable==========================================
    // Child Component
    [SerializeField] protected BaseSkill skill1;
    [SerializeField] protected BaseSkill skill2;

}
