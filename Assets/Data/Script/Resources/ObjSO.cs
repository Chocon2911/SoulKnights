using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjSO : ScriptableObject
{
    //==========================================Variable==========================================
    [Header("Obj")]
    [SerializeField] protected string id;
    [SerializeField] protected string objName;

    //============================================Get=============================================
    public string Id => id;
    public string ObjName => objName;
}
