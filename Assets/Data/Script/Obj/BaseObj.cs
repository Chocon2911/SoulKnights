using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObj : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Obj")]
    [SerializeField] protected string id;
    [SerializeField] protected string objName;

    //==========================================Get Set===========================================
    public string Id
    {
        get => id;
        set => id = value;
    }

    public string ObjName
    {
        get => objName;
        set => ObjName = value;
    }

    //===========================================Other============================================
    protected abstract void DefaultStat();
}
