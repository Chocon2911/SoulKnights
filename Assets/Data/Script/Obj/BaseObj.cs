using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObj : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Base Obj")]
    // Stat
    [SerializeField] protected string id;
    [SerializeField] protected string objName;

    // Unity Component
    [SerializeField] protected SpriteRenderer model;

    //==========================================Get Set===========================================
    public string Id
    {
        get => id;
        set => id = value;
    }
    public string ObjName
    {
        get => objName;
        set => objName = value;
    }

    public SpriteRenderer Model
    {
        get => model;
    }
}
