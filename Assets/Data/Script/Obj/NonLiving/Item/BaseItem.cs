using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseItem : NonLivingObj
{
    [Header("Base Item")]
    [SerializeField] protected ItemType itemType;
}
